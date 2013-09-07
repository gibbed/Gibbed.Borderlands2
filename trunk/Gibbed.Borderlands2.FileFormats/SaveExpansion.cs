/* Copyright (c) 2013 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Gibbed.Borderlands2.ProtoBufFormats.WillowTwoSave;

namespace Gibbed.Borderlands2.FileFormats
{
    public static class SaveExpansion
    {
        private static readonly byte[] _HackInventorySerialNumber = new byte[]
        {
            0x07, 0x00, 0x00, 0x00, 0x00, 0x39, 0x2A, 0xFF,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        private static void AddExpansionSavedataToUnloadableItemData(this WillowTwoPlayerSaveGame saveGame,
                                                                     byte id,
                                                                     int value1,
                                                                     int value2)
        {
            saveGame.PackedItemData.Add(new PackedItemData()
            {
                InventorySerialNumber = (byte[])_HackInventorySerialNumber.Clone(),
                Quantity = -(id | (Math.Max(0, Math.Min(value1, 0x7FFFFF)) << 8)),
                Mark = value2,
            });
        }

        private static void AddExpansionSavedataToUnloadableItemData(this WillowTwoPlayerSaveGame saveGame,
                                                                     byte id,
                                                                     int value1)
        {
            AddExpansionSavedataToUnloadableItemData(saveGame, id, value1, 0);
        }

        public static void AddExpansionSavedataToUnloadableItemData(WillowTwoPlayerSaveGame saveGame)
        {
            if (saveGame == null)
            {
                throw new ArgumentNullException("saveGame");
            }

            if (saveGame.PackedItemData == null)
            {
                saveGame.PackedItemData = new List<PackedItemData>();
            }
            else
            {
                var oldHacks = saveGame.PackedItemData.Where(pid => pid.Quantity < 0).ToArray();
                foreach (var hack in oldHacks)
                {
                    var type = (-hack.Quantity) & 0xFF;
                    if (type >= 1 && type <= 5)
                    {
                        saveGame.PackedItemData.Remove(hack);
                    }
                }
            }

            if (saveGame.CurrencyOnHand.Count >= 1 &&
                saveGame.CurrencyOnHand[1] > 99)
            {
                var extraEridium = saveGame.CurrencyOnHand[1] - 99;
                saveGame.AddExpansionSavedataToUnloadableItemData(1, extraEridium);
                saveGame.CurrencyOnHand[1] = 99;
            }

            if (saveGame.PlaythroughsCompleted > 1 &&
                saveGame.LastPlaythroughNumber >= 1 && saveGame.LastPlaythroughNumber <= 0x800000)
            {
                var extraLastPlaythroughNumber = saveGame.LastPlaythroughNumber - 1;
                var extraPlaythroughsCompleted = (byte)(saveGame.PlaythroughsCompleted - 1);
                saveGame.AddExpansionSavedataToUnloadableItemData(2,
                                                                  extraLastPlaythroughNumber,
                                                                  extraPlaythroughsCompleted);
                saveGame.LastPlaythroughNumber = saveGame.LastPlaythroughNumber >= 1 ? 1 : 0;
                saveGame.PlaythroughsCompleted = 1;
            }

            if (saveGame.PlayerHasPlayedInPlaythroughThree.HasValue == true)
            {
                var value = saveGame.PlayerHasPlayedInPlaythroughThree.Value;
                saveGame.AddExpansionSavedataToUnloadableItemData(3, value);
            }

            if (saveGame.NumOverpowerLevelsUnlocked.HasValue == true)
            {
                var value = saveGame.NumOverpowerLevelsUnlocked.Value;
                saveGame.AddExpansionSavedataToUnloadableItemData(4, value);
            }

            if (saveGame.LastOverpowerChoice.HasValue == true)
            {
                var value = saveGame.LastOverpowerChoice.Value;
                saveGame.AddExpansionSavedataToUnloadableItemData(5, value);
            }
        }

        private static void ExtractExpansionSavedataFromUnloadableItemData(PackedItemData packedItemData,
                                                                           out byte id,
                                                                           out int value1,
                                                                           out int value2)
        {
            var dummy = -packedItemData.Quantity;
            id = (byte)(dummy & 0xFF);
            value1 = (dummy >> 8) & 0x7FFFFF;
            value2 = packedItemData.Mark;
        }

        public static void ExtractExpansionSavedataFromUnloadableItemData(WillowTwoPlayerSaveGame saveGame)
        {
            if (saveGame == null)
            {
                throw new ArgumentNullException("saveGame");
            }

            if (saveGame.PackedItemData != null)
            {
                var hacks = saveGame.PackedItemData.Where(pid => pid.Quantity < 0).ToArray();
                foreach (var hack in hacks)
                {
                    byte id;
                    int value1, value2;
                    ExtractExpansionSavedataFromUnloadableItemData(hack, out id, out value1, out value2);

                    if (id == 1)
                    {
                        if (saveGame.CurrencyOnHand.Count >= 1)
                        {
                            saveGame.CurrencyOnHand[1] += value1;
                        }
                        saveGame.PackedItemData.Remove(hack);
                    }
                    else if (id == 2)
                    {
                        saveGame.LastPlaythroughNumber += value1;
                        saveGame.PlaythroughsCompleted += (byte)value2;
                        saveGame.PackedItemData.Remove(hack);
                    }
                    else if (id == 3)
                    {
                        saveGame.PlayerHasPlayedInPlaythroughThree = value1;
                        saveGame.PackedItemData.Remove(hack);
                    }
                    else if (id == 4)
                    {
                        saveGame.NumOverpowerLevelsUnlocked = value1;
                        saveGame.PackedItemData.Remove(hack);
                    }
                    else if (id == 5)
                    {
                        saveGame.LastOverpowerChoice = value1;
                        saveGame.PackedItemData.Remove(hack);
                    }
                }
            }
        }
    }
}
