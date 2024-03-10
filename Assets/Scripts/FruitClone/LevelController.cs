using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FruitClone
{
    public class LevelController : MonoBehaviour
    {
        public static int CurrentLevel { get; private set; }

        [SerializeField] private float waitBetweenSpawns;
        [SerializeField] private float waitBetweenChallenges;
        [SerializeField] private float waitBetweenWaves;
        [SerializeField] private int spawnAmount;
        [SerializeField] private int wavesPerLevel = 5;

        [Header("Spawners")]
        [SerializeField] private Spawner bottomLeftSpawner;
        [SerializeField] private Spawner bottomRightSpawner;
        [SerializeField] private Spawner leftSpawner;
        [SerializeField] private Spawner rightSpawner;
        [SerializeField] private Spawner middleLeftSpawner;
        [SerializeField] private Spawner middleMiddleSpawner;
        [SerializeField] private Spawner middleRightSpawner;
        [SerializeField] private Spawner leftNoGravitySpawner;
        [SerializeField] private Spawner rightNoGravitySpawner;

        private List<Challenge> _challenges = new();
        private int _currentWave = 0;

        private async void Start()
        {
            await Task.Delay(5000);
            SpawnWave();
        }

        private void SpawnWave()
        {
            CreateChallenges();
            StartCoroutine(SpawnChallenges());
        }

        private IEnumerator SpawnChallenges()
        {
            for(int i = 0; i < _challenges.Count; i++)
            {
                switch (_challenges[i])
                {
                    #region Bottom Corner Sequence

                    case Challenge.BottomCornerSequence:

                        int corner = Random.Range(0, 2);
                        Spawner selectedSpawner = corner == 0 ? bottomLeftSpawner : bottomRightSpawner;

                        for (int j = 0; j < spawnAmount; j++)
                        {
                            selectedSpawner.Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns);
                        }
                        break;

                    #endregion

                    #region Bottom Aligned

                    case Challenge.BottomAligned:

                        int dir = Random.Range(0, 2);
                        Spawner firstSpawner;
                        Spawner lastSpawner;

                        if(dir == 0)
                        {
                            firstSpawner = middleLeftSpawner;
                            lastSpawner = middleRightSpawner;
                        }
                        else
                        {
                            firstSpawner = middleRightSpawner;
                            lastSpawner = middleLeftSpawner;
                        }

                        Spawner[] spawners = new Spawner[3] {firstSpawner, middleMiddleSpawner, lastSpawner };

                        for(int k = 0; k < spawnAmount; k++)
                        {
                            spawners[k % spawners.Length].Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns);
                        }

                        break;

                    #endregion

                    #region Bottom Corner Alternate

                    case Challenge.BottomCornerAlternate:

                        for(int l = 0; l < spawnAmount; l++)
                        {
                            if(l % 2 == 0)
                                bottomLeftSpawner.Spawn();
                            else
                                bottomRightSpawner.Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns * 4);
                        }

                        break;

                    #endregion

                    #region LateralSequence

                    case Challenge.LateralSequence:

                        int lateral = Random.Range(0, 2);
                        Spawner selectedLateral = lateral == 0 ? leftSpawner : rightSpawner;

                        for (int m = 0; m < spawnAmount; m++)
                        {
                            selectedLateral.Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns);
                        }
                        break;

                    #endregion

                    #region Lateral Alternate

                    case Challenge.LateralAlternate:

                        for (int n = 0; n < spawnAmount; n++)
                        {
                            if (n % 2 == 0)
                                leftSpawner.Spawn();
                            else
                                rightSpawner.Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns * 4);
                        }

                        break;

                    #endregion

                    #region Lateral Sequence No Gravity

                    case Challenge.LateralSequenceNoGravity:

                        int lateralNoGravity = Random.Range(0, 2);
                        Spawner selectedLateralNG = lateralNoGravity == 0 ? leftNoGravitySpawner : rightNoGravitySpawner;

                        for (int m = 0; m < spawnAmount; m++)
                        {
                            selectedLateralNG.Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns);
                        }

                        break;

                    #endregion

                    case Challenge.LateralAlternateNoGravity:

                        for (int n = 0; n < spawnAmount; n++)
                        {
                            if (n % 2 == 0)
                                leftNoGravitySpawner.Spawn();
                            else
                                rightNoGravitySpawner.Spawn();
                            yield return new WaitForSeconds(waitBetweenSpawns * 4);
                        }

                        break;
                }
                yield return new WaitForSeconds(waitBetweenChallenges);
            }

            _currentWave++;

            if(_currentWave >= wavesPerLevel)
            {
                _currentWave = 0;
                CurrentLevel++;
            }

            yield return new WaitForSeconds(waitBetweenSpawns);

            SpawnWave();
        }

        private void CreateChallenges()
        {
            _challenges.Clear();

            Challenge currentChallenges = (Challenge)CurrentLevel;

            foreach (Challenge challenge in Enum.GetValues(typeof(Challenge)))
                if (currentChallenges.HasFlag(challenge))
                    _challenges.Add(challenge);

            _challenges = Shuffle(_challenges);
        }

        public List<T> Shuffle<T>(List<T> list)
        {
            System.Random random = new System.Random();

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                T value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
            return list;
        }
    }

    [Flags]
    public enum Challenge
    {
        BottomCornerSequence = 1,
        BottomAligned = 2,
        BottomCornerAlternate = 4,
        LateralSequence = 8,
        LateralAlternate = 16,
        LateralSequenceNoGravity = 32,
        LateralAlternateNoGravity = 64
    }
}
