using Modding;
using System.Collections.Generic;
using UnityEngine;

namespace PogoableFlukes {
    public class PogoableFlukes: Mod {
        new public string GetName() => "PogoableFlukes";
        public override string GetVersion() => "1.0.0.0";
        
        List<GameObject> flukes = new();

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            On.SpellFluke.OnEnable += setFlukeLayer;
        }

        private void setFlukeLayer(On.SpellFluke.orig_OnEnable orig, SpellFluke self) {
            orig(self);
            self.gameObject.layer = LayerMask.NameToLayer("Attack");
            flukes.Add(self.gameObject);
            for(int i = 0; i < flukes.Count; i++) {
                if(flukes[i].activeSelf) {
                    Physics2D.IgnoreCollision(flukes[i].GetComponent<Collider2D>(), self.gameObject.GetComponent<Collider2D>());
                }
                else {
                    flukes.Remove(flukes[i--]);
                }
            }
        }
    }
}