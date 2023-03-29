// NoSleepingLoadingScreen, Valheim mod that disables the loading screen when sleeping
// Copyright (C) 2022 BurgersMcFly
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace NoSleepingLoadingScreen
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    public class NoSleepingLoadingScreen : BaseUnityPlugin
    {
        const string pluginGUID = "NoSleepingLoadingScreen";
        const string pluginName = "No Sleeping Loading Screen";
        const string pluginVersion = "1.0.0";
        public static readonly Harmony harmony = new Harmony("NoSleepingLoadingScreen");
        void Awake()
        {
            harmony.PatchAll();

        }
        void OnDestroy()
        {
            harmony.UnpatchSelf();
        }


        [HarmonyPatch(typeof(Hud), "UpdateBlackScreen")]
        public static class RemoveLoadingScreen
        {
            private static void Prefix(ref CanvasGroup ___m_loadingScreen, ref Player player,ref GameObject ___m_sleepingProgress)
            {
                if (player != null && player.IsSleeping())
                {
                    ___m_sleepingProgress.SetActive(value: false);
                    ___m_loadingScreen.alpha = 0f;
                }
            }

        }
    }
}