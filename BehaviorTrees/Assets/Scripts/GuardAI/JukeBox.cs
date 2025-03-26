using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class JukeBox : Node
{
    private Transform _transform;
    private float _musicRange;
    private Transform _currJukebox = null;
    private bool isPlayingMusic = false;
    private static int _musicLayerMask = 1 << 9; //Jukebox layer

    public JukeBox(Transform transform, float musicRange)
    {
        _transform = transform;
        _musicRange = musicRange;
    }

    public override NodeState Evaluate()
    {
        if (isPlayingMusic)
        {
            return NodeState.RUNNING;
        }

        Collider[] hitColliders = Physics.OverlapSphere(_transform.position, _musicRange, _musicLayerMask);
        if (hitColliders.Length > 0)
        {
            _currJukebox = hitColliders[0].transform;
            _transform.position = Vector3.MoveTowards(_transform.position, _currJukebox.position, 2f * Time.deltaTime);

            if (Vector3.Distance(_transform.position, _currJukebox.position) < 0.5f)
            {
                _transform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(PlayMusic());
                isPlayingMusic = true;
                return NodeState.RUNNING;
            }

            return NodeState.RUNNING;
        }

        return NodeState.FAILURE;
    }

    private IEnumerator PlayMusic()
    {
        Debug.Log("Listening to music...");
        yield return new WaitForSeconds(5f);
        Debug.Log("Music over. Destroying jukebox.");
        
        if (_currJukebox != null)
        {
            GameObject.Destroy(_currJukebox.gameObject);
        }

        isPlayingMusic = false;
    }
}