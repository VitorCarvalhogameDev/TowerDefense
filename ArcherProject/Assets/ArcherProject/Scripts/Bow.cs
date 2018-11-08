using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomScripts
{
    public class Bow : MonoBehaviour
    {
        public LineRenderer topLine, botLine;
        public Transform TopPos, MidPos, BotPos;
        public Transform HandPos;

        public bool playerPushedString;
        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update()
        {
            if (!playerPushedString)
            {
                topLine.SetPosition(0, TopPos.position);
                topLine.SetPosition(1, MidPos.position);

                botLine.SetPosition(0, BotPos.position);
                botLine.SetPosition(1, MidPos.position);
            }
            else
            {
                topLine.SetPosition(0, TopPos.position);
                topLine.SetPosition(1, HandPos.position);

                botLine.SetPosition(0, BotPos.position);
                botLine.SetPosition(1, HandPos.position);
            }
        }
    }
}
