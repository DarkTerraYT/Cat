using MelonLoader;
using BTD_Mod_Helper;
using Cat;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppNinjaKiwi.NKMulti;
using BTD_Mod_Helper.Api.Coop;
using Il2CppNinjaKiwi.NKMulti.IO;
using System.IO;
using Il2CppAssets.Scripts.Unity.Network;

[assembly: MelonInfo(typeof(Cat.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Cat;

public class Main : BloonsTD6Mod
{
    public override void OnWeaponFire(Weapon weapon)
    {
        var tower = weapon.attack.tower;
        if(tower != null && tower.towerModel.baseId == ModContent.TowerID<Cat>())
        {
            var towerDisplay = tower.GetUnityDisplayNode();
            if(towerDisplay)
            {
                towerDisplay.animationComponent.SetTrigger("Attack");
            }
        }
    }

    public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
    {
        if(newBaseTowerModel.baseId == ModContent.TowerID<Cat>())
        {
            var towerDisplay = tower.GetUnityDisplayNode();
            if(towerDisplay)
            {
                towerDisplay.animationComponent.SetInteger("Tier", newBaseTowerModel.tier);
            }
        }
    }
}