using UnityEngine;

namespace Mod {
    public class SuperBombBehaviour : MonoBehaviour {
        void Update() {
            var phys = GetComponent<PhysicalBehaviour>();
            if (phys.OnFire) {
                ExplosionCreator.Explode(transform.position, 10000000f);
                Destroy(gameObject);
            }
        }
    }

    public class Mod {
        public static void Main() {
            var original = ModAPI.FindSpawnable("Dynamite");
            if (original == null) return;
            
            ModAPI.Register(new Modification() {
                OriginalItem = original,
                NameOverride = "Green Bomb",
                DescriptionOverride = ".....?",
                CategoryOverride = ModAPI.FindCategory("Explosives"),
                ThumbnailOverride = ModAPI.LoadSprite("bomb_icon.png"),
                AfterSpawn = (Instance) => {
                    var sprite = Instance.GetComponent<SpriteRenderer>();
                    sprite.sprite = ModAPI.LoadSprite("bomb.png");  
                    sprite.transform.localScale = new Vector3(3f, 3f, 1f);  
                    
                    var phys = Instance.GetComponent<PhysicalBehaviour>();
                    phys.InitialMass = 10f;
                    
                    
                    Instance.FixColliders();
                    
                    Instance.AddComponent<SuperBombBehaviour>();
                }
            });
        }
    }
}