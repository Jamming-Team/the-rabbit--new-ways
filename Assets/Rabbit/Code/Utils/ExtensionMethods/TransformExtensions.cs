using System.Collections.Generic;
using UnityEngine;

namespace Rabbit {
    public static class TransformExtensions {
        public static IEnumerable<Transform> Children(this Transform parent) {
            foreach (Transform child in parent) {
                yield return child;
            }
        }
        
        

        // public static void GetTopLvlChildren<T>(this Transform parent, ref List<T> children) {
        //     // children = new List<Transform>();
        //     for (int i = 0; i < parent.childCount; ++i) {
        //         var child = parent.GetChild(i);
        //         if (child.parent != parent)
        //             break;
        //         
        //         children.Add((IState)child);
        //     }
        //
        // }
        
        public static void DestroyChildren(this Transform parent) {
            parent.PerformActionOnChildren(child => Object.Destroy(child.gameObject));
        }

        public static void EnableChildren(this Transform parent) {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(true));
        }

        public static void DisableChildren(this Transform parent) {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(false));
        }

        static void PerformActionOnChildren(this Transform parent, System.Action<Transform> action) {
            for (var i = parent.childCount - 1; i >= 0; i--) {
                action(parent.GetChild(i));
            }
        }
    }
}