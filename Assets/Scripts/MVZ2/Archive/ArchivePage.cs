﻿using UnityEngine;

namespace MVZ2.Archives
{
    public abstract class ArchivePage : MonoBehaviour
    {
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
