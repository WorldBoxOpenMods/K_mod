namespace K_mod
{
    class projectile
    {
        public static void init()
        {
            var bolt = AssetManager.projectiles.clone("bolt", "arrow");
            bolt.id = "bolt";
            bolt.sound_launch = "event:/SFX/WEAPONS/WeaponGreenOrbStart";
            bolt.sound_impact = "event:/SFX/WEAPONS/WeaponGreenOrbLand";
            bolt.speed = 30f;
            bolt.look_at_target = true;
            bolt.parabolic = false;
            AssetManager.projectiles.add(bolt);

            var gun = AssetManager.projectiles.clone("gun", "arrow");
            gun.id = "gun";
            gun.texture = "gun";
            gun.sound_launch = "event:/SFX/WEAPONS/WeaponGreenOrbStart";
            gun.sound_impact = "event:/SFX/WEAPONS/WeaponGreenOrbLand";
            gun.speed = 20f;
            gun.look_at_target = true;
            gun.parabolic = false;
            AssetManager.projectiles.add(gun);

            var stone = AssetManager.projectiles.clone("stone", "arrow");
            stone.id = "stone";
            stone.texture = "stone";
            stone.sound_launch = "event:/SFX/WEAPONS/WeaponGreenOrbStart";
            stone.sound_impact = "event:/SFX/WEAPONS/WeaponGreenOrbLand";
            //stone.startScale = 0.25f;
            stone.texture_shadow = "shadow_ball";
            // stone.endEffect = "fx_bad_place";
            stone.rotate = true;
            stone.speed = 13f;
            stone.terraformRange = 3;
            stone.parabolic = true;
            AssetManager.projectiles.add(stone);

            var Ballista_Arrow = AssetManager.projectiles.clone("Ballista_Arrow", "arrow");
            Ballista_Arrow.id = "Ballista_Arrow";
            Ballista_Arrow.texture = "Ballista_Arrow";
            Ballista_Arrow.sound_launch = "event:/SFX/WEAPONS/WeaponGreenOrbStart";
            Ballista_Arrow.sound_impact = "event:/SFX/WEAPONS/WeaponGreenOrbLand";
            Ballista_Arrow.speed = 20f;
            Ballista_Arrow.parabolic = true;
            AssetManager.projectiles.add(Ballista_Arrow);

            var javelin = AssetManager.projectiles.clone("javelin", "arrow");
            javelin.id = "javelin";
            javelin.texture = "iconJavelin";
            javelin.sound_launch = "event:/SFX/WEAPONS/WeaponGreenOrbStart";
            javelin.sound_impact = "event:/SFX/WEAPONS/WeaponGreenOrbLand";
            javelin.speed = 14f;
            javelin.startScale = 0.06f;
            javelin.targetScale = 0.06f;
            javelin.parabolic = true;
            AssetManager.projectiles.add(javelin);

        }
    }
}
