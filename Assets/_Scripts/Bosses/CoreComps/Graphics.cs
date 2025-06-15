using System.Collections.Generic;
using Bardent.Assets._Scripts.Bosses;
using Bardent.CoreSystem;
using UnityEngine;

namespace Bardent
{
    public class Graphics : CoreComponent
    {

        [SerializeField] public List<Graphic> graphicList;

        void Start()
        {

        }

        // Update is called once per frame
        public override void LogicStart()
        {

        }

        public override void LogicUpdate()
        {
            // Nada
        }

        public void AddGraphic(Graphic graphic)
        {
            graphicList.Add(graphic);
        }

        public BossBase GetBoss()
        {
            //core.
            return null;
        }

        public void SetAnimationVariable(string variable, bool value)
        {
            foreach (Graphic graphic in graphicList)
            {
                graphic.animator.SetBool(variable, value);
            }
        }
    }

}
