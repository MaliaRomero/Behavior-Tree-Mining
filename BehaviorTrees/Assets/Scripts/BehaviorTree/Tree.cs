using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {

        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree(); // Build according ctionto set up tree fun
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate(); //If there is a tree, continuously evaluate
        }

        protected abstract Node SetupTree();

    }

}
