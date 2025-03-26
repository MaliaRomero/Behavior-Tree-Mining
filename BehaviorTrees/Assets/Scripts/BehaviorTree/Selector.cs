using System.Collections.Generic;

namespace BehaviorTree
{
    // Millington- "A Selector will return immediately with a success
    //status code when one of its children runs successfully. As long
    //as its children are failing, it will keep on trying. If it runs
    // out of children completely, it will return a failure status code.
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }

}
