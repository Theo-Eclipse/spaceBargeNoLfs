using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flier;

namespace RewardSystem
{
    public class DestroyReward : MonoBehaviour
    {
        public int rewardAmount = 350;
        [SerializeField] private BasicFlier enemyInstance;
        // Start is called before the first frame update
        void Start()
        {
            enemyInstance.onFlierDestroyed.AddListener(AddRewardToPlayer);
        }
        private void AddRewardToPlayer() 
        {
            PlayerStats.instance.playerScore += rewardAmount;
        }
    }
}
