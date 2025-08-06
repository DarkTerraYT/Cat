using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Audio;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cat
{
    public class Cat : ModTower
    {
        public override TowerSet TowerSet => TowerSet.Primary;

        public override string BaseTower => TowerType.DartMonkey;

        public override int Cost => 150;

        public override string Icon => "Icon";

        public override string Description => "Does a spin and impresses the bloons! Can see camo bloons.";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range = 25;
            towerModel.GetAttackModel().range = 25;

            var placeSounds = towerModel.GetBehavior<CreateSoundOnTowerPlaceModel>();
            placeSounds.sound1 = new("Cat_Place1", GetAudioClipReference("place1"));
            placeSounds.sound2 = new("Cat_Place2", GetAudioClipReference("place2"));

            var upgradeSounds = towerModel.GetBehavior<CreateSoundOnUpgradeModel>();
            upgradeSounds.sound = placeSounds.sound1;
            upgradeSounds.sound1 = placeSounds.sound1;
            upgradeSounds.sound2 = placeSounds.sound2;
            upgradeSounds.sound3 = placeSounds.sound1;
            upgradeSounds.sound4 = placeSounds.sound2;
            upgradeSounds.sound5 = placeSounds.sound1;
            upgradeSounds.sound6 = placeSounds.sound2;
            upgradeSounds.sound7 = placeSounds.sound1;
            upgradeSounds.sound8 = placeSounds.sound2;
            
            towerModel.GetBehavior<CreateSoundOnSellModel>().sound = new SoundModel("Cat_Sell1", GetAudioClipReference("sell1"));

            var weapon = towerModel.GetWeapon();
            weapon.AddBehavior(new CreateSoundOnProjectileCreatedModel("CreateSoundOnProjectileCreatedModel_", 
                new SoundModel("Cat_Attack1", GetAudioClipReference("attack1")),
                new SoundModel("Cat_Attack2", GetAudioClipReference("attack2")),
                new SoundModel("Cat_Attack3", GetAudioClipReference("attack3")), 
                new SoundModel("Cat_Attack4", GetAudioClipReference("attack4")), 
                new SoundModel("Cat_Attack5", GetAudioClipReference("attack5")), ""));
            var proj = weapon.projectile;
            proj.RemoveBehavior<TravelStraitModel>();
            proj.AddBehavior(new AgeModel("AgeModel_", 2, 1, false, null));
            proj.pierce = 5;
            proj.ApplyDisplay<Stars1>();
            weapon.rate = 2;

            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(filter => filter.isActive = false);
        }
    }

    public class RadicalRolls : ModUpgrade<Cat>
    {
        public override int Tier => 1;

        public override int Path => MIDDLE;

        public override string Portrait => Icon;

        public override string Description => "Does a roll instead of a spin! The cat can do tricks more often and impress more bloons per trick.";

        public override int Cost => 120;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetWeapon();
            weapon.rate *= 0.8f;
            weapon.projectile.pierce += 5;
        }
    }
    public class BountifulBackflips : ModUpgrade<Cat>
    {
        public override int Tier => 2;

        public override string Portrait => Icon;

        public override int Path => MIDDLE;

        public override string Description => "Upgrades the trick even further to add a jump making a backflip! Does more damage and does tricks even more often.Adds a cute cat to the game, who does cool tricks to impress the bloons!\r\n\r\nWhat does the cat say? Not ding-ding-ding ding-ding ding-ding-ding-ding.\r\n\r\nMore paths will be added if I see enough interest in them.";

        public override int Cost => 450;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetWeapon();
            weapon.rate *= 0.8f;
            weapon.projectile.ApplyDisplay<Stars2>();
            weapon.projectile.GetDamageModel().damage += 1;
        }
    }
    public class BetterBackflips : ModUpgrade<Cat>
    {
        public override int Tier => 3;

        public override int Path => MIDDLE;
        public override string Portrait => Icon;

        public override string Description => "Does a spin while doing a backflip! Cat impresses bloons for two times longer, does more damage, AND can impress lead and ice bloons through their thick shells.";

        public override int Cost => 1550;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetWeapon();
            weapon.rate *= 0.8f;
            weapon.projectile.ApplyDisplay<Stars2>();
            weapon.projectile.GetDamageModel().damage += 3;
            weapon.projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;
            weapon.projectile.GetBehavior<AgeModel>().lifespan *= 2;
        }
    }
    public class BeautifulBackflips : ModUpgrade<Cat>
    {
        public override int Tier => 4;

        public override int Path => MIDDLE;
        public override string Portrait => Icon;

        public override string Description => "Does a spin on two axis while doing a backflip! Backflips one after another! Includes other general buffs.";

        public override int Cost => 8150;

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetWeapon();
            weapon.projectile.GetDamageModel().damage += 3;
            weapon.projectile.pierce += 15;
            weapon.projectile.GetBehavior<AgeModel>().lifespan *= 2;
            weapon.projectile.ApplyDisplay<Stars3>();
            weapon.rate = 1;
        }
    }

    public class NiceCar : ModUpgrade<Cat>
    {
        public override int Path => MIDDLE;

        public override int Tier => 5;

        public override int Cost => 100000;

        public override string Description => "Hits the bloons with a 5000 ton death machine going at 100 miles\\h.\"Hey, that bump is shaped like a deer!\"\n\nDo not hit people with cars in real life, this is a silly game about monkeys and bloons. (DJD beat me to it)";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range = 75;
            towerModel.GetAttackModel().range = 75;

            var weapon = towerModel.GetWeapon();

            var attackSound = new SoundModel("Cat_Attack6", GetAudioClipReference("attack6"));

            var atkSounds = weapon.GetBehavior<CreateSoundOnProjectileCreatedModel>();
            atkSounds.sound1 = attackSound;
            atkSounds.sound2 = attackSound;
            atkSounds.sound3 = attackSound;
            atkSounds.sound4 = attackSound;
            atkSounds.sound5 = attackSound;

            var proj = weapon.projectile;
            proj.radius = 15;
            proj.RemoveBehavior<AgeModel>();
            proj.AddBehavior(new TravelStraitModel("AgeModel_", 3000, 0.5f));
            proj.pierce = 9999999;
            proj.display = new PrefabReference();
            proj.GetDamageModel().damage = 9999999;
            weapon.rate = 0.33f;

        }
    }

    public class Stars1 : ModCustomDisplay
    {
        public override string AssetBundleName => "cat";

        public override Il2CppAssets.Scripts.Simulation.SMath.Vector3 PositionOffset => new(0, 0, 5);

        public override string PrefabName => "Stars1";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetComponent<ParticleSystem>().Play();
        }
    }
    public class Stars2 : ModCustomDisplay
    {
        public override string AssetBundleName => "cat";

        public override Il2CppAssets.Scripts.Simulation.SMath.Vector3 PositionOffset => new(0, 0, 5);

        public override string PrefabName => "Stars2";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetComponent<ParticleSystem>().Play();
        }
    }
    public class Stars3 : ModCustomDisplay
    {
        public override string AssetBundleName => "cat";

        public override Il2CppAssets.Scripts.Simulation.SMath.Vector3 PositionOffset => new(0, 0, 5);

        public override string PrefabName => "Stars3";

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.transform.GetComponent<ParticleSystem>().Play();
        }
    }

    public class CatDisplay : ModTowerCustomDisplay<Cat>
    {
        public override string AssetBundleName => "cat";

        public override string PrefabName => "Cat";

        public override bool UseForTower(params int[] tiers) => true;

        private GameObject Shadow;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.GetMeshRenderer().ApplyOutlineShader();
            node.GetMeshRenderer().SetOutlineColor(new Color32(26, 26, 24, 255));
            PrefabReference display = Game.instance.model.GetTowerWithName(TowerType.DartMonkey).display;
            Game.instance.GetDisplayFactory().FindAndSetupPrototypeAsync(display, DisplayCategory, (Action<UnityDisplayNode>)delegate (UnityDisplayNode udn)
            {
                var obj = udn.transform.GetChild(0).GetChild(1).gameObject;
                Shadow = obj.Duplicate();
                Shadow.transform.SetParent(node.transform, false);
            });
        }
    }
}
