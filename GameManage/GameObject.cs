using System;
namespace TikTakToe.GameManage
{
    public abstract class GameObject : IGameObject
    {
        public int CoordinatorNo { get; set; }

        public String AssetText { get; set; }

        public int GameObjectCode { get; set; }

        public GameObject(Int16 coordinateNo)
        {
            this.CoordinatorNo = coordinateNo;
        }

        public GameObject()
        {

        }
    }
}
