using RPG.Saving;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B,C
        };

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            savingWrapper.Load();
            
            Portal destinationPortal = GetDestinationPortal();
            
            UpdatePlayer(destinationPortal);
            
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal destinationPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            NavMeshAgent navMeshAgent = player.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            navMeshAgent.Warp(destinationPortal.spawnPoint.position);
            player.transform.rotation = destinationPortal.spawnPoint.rotation;
            navMeshAgent.enabled = true;
        }

        private Portal GetDestinationPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) { continue; }
                if (this.destination == portal.destination)
                {
                    return portal;
                }
            }
            return null;

        }
    }
}