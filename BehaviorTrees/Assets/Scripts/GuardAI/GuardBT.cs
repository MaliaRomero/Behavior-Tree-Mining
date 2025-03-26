using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float attackRange = 1f;
    public static float mineRange = 2f;
    public static float radRange = 3f;
    public static float musicRange = 3f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckMineSpotInRange(transform, mineRange),
                // new TaskMine(transform),  // Mine it if found
            }),

            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new Sequence(new List<Node>
            {
                new RadSpot(transform, radRange),
            }),
            new Sequence(new List<Node>
            {
                new JukeBox(transform, musicRange),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
