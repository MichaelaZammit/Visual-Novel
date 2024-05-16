using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PositionedSprite
{
        [field:SerializeField]
        public Sprite Sprite { get; private set; }
        [field:SerializeField]
        public Vector3 PositionModifier { get; set; }
}
