using System;
using System.Reflection;
using UnityEngine;

namespace Rabbit {
    public class DataManager : IVisitor {
        GameDataSO _dataSO;

        public DataManager(GameDataSO dataSO) {
            _dataSO = dataSO;
        }
        
        public void TrySupply(IVisitable requester) {
            requester.Accept(this);
        }
        
        // --- Suppliers --- 

        public void Visit(object o) {
            var visitMethod = GetType().GetMethod("Visit", new Type[] { o.GetType() });
            if (visitMethod != null && visitMethod != GetType().GetMethod("Visit", new Type[] { typeof(object) }))
                visitMethod.Invoke(this, new object[] { o });
            else
                DefaultVisit(o);
        }

        void DefaultVisit(object o) {
            // noop (== `no op` == `no operation`)
            Debug.Log("MCDataFillerVisitor.DefaultVisit");
        }
        
        public void Visit(GP_SceneController requester) {
            requester.data = _dataSO.content;
        }
    }
}