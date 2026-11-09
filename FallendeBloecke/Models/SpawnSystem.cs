using System;
using System.Collections.Generic;
using System.Text;

namespace FallendeBloecke.Models
{
    public class SpawnSystem
    {
        private float DefaultSpawnTime;
        private float SpawnTime;
        
        public SpawnSystem(float spawnTime)
        {
            DefaultSpawnTime = spawnTime;
            SpawnTime = DefaultSpawnTime;
        }

        public bool CanSpawn()
        {
            if (SpawnTime <= 0.0f)
            {
                ResetSpawnTime();

                return true;
            }
            return false;
        }

        public void Update(float dt)
        {
            this.SpawnTime -= dt;
        }

        private void ResetSpawnTime()
        {
            this.SpawnTime = DefaultSpawnTime;
        }
    }
}
