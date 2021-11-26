# BetterDoggie
Improves upon the base game SCP-939.

Many of the features are from the original [BetterScp939](https://github.com/iopietro/BetterScp939)

---

This plugin applies damage based on the dogs hume shield. The lower the hume shield the higher the damage.

The dog will by default be sped up with the 207 effect but this can be configured and will be slowed down when attacking.
It can also be configured for the dog to bust down doors if it reaches under a certain hume amount.

Configs | Description
:---: | :---: 
DoggieHealth | The max HP that dog will spawn with.
DoggieAhp | The maximum AHP the dog can spawn with.
ColaSpeedBoost | Should the dog get the SCP-207 effect?
DoggieScale | The size of the dog when it spawns.
BaseDamage | The base amount of damage the dog will do.
MaxDamageBoost | The maximum amount of additional damage the dog can deal.
SpawnBroadcast | Message to send to players when they spawn as the dog.
EnableDogDoorBusting | Can 939 bust open doors and gates if it is below a certain AHP?
DoorBustAhp | The dog has to have an AHP lower than this to bust doors.
EnableBustSpeedBoost | Gives 939 a speed boost when it busts down a door.
BustBoostAmount | The amount of coke to give the dog when he busts down a door. (Max of 4)