using System;
using System.Collections.Generic;
using System.Text;

namespace FallendeBloecke.Models
{
    public class SpawnSystem
    {
        private float DefaultSpawnTime;
        private float SpawnTime =0.0f;
        
        public SpawnSystem(float spawnTime)
        {
            this.DefaultSpawnTime = spawnTime;
        }

        public bool CanSpawn()
        {
            if (SpawnTime <= 0.0f)
            {
                ResetTimeSpawnNext();

                return true;
            }
            return false;
        }

        public void Update(float dt)
        {
            this.SpawnTime -= dt;
        }

        private void ResetTimeSpawnNext()
        {
            this.SpawnTime = DefaultSpawnTime;
        }
    }
}
