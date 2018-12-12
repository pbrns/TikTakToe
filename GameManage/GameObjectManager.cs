using System;
using System.Collections.Generic;
using System.Linq;

namespace TikTakToe.GameManage
{
    /*
     * 0   1    2   3
       4   5    6   7
       8   9   10  11
       12 13   14  15

        algorithm for diagonial       
        right to left = size -1 (3) for each size of matrix 3, 6, 9, 12
        left to right = size +1 (5) for each size of matrix 0,5,10,15

        0 1 2
        3 4 5
        6 7 8

        algorithm for diagonial              
        right to left = size -1 (2) for each size of matrix 2, 4, 6,
        left to right = size +1 (4) for each size of matrix 0,4,8

        matrix size need to be square 3x3, 4x4 etc.       
     */

    public class GameObjects
    {
        public int CoordinateNo { get; set; }
        public GameObject GameObject { get; set; }
    }

    public class GameObjectManager : IGameObjectManager
    {
        private GameObject lastUsedGameObject;
        public GameObject LastUsedGameObject
        {
            get { return lastUsedGameObject; }
        }


        public Action<bool> WinAction { get; set; }

        GameObjects[] gameObjectsStorage = new GameObjects[9];

        private int matrixLinearSize = 3;

        private enum DiagonialSide
        {
            RightToLeft = 1,
            LeftToRight = 2,
        };

        public void Initiliaze()
        {
            lastUsedGameObject = null;
            gameObjectsStorage = new GameObjects[9];
        }

        public void AddGameObjectInGame(IGameObject gameObject)
        {
            lastUsedGameObject = (GameObject)gameObject;

            gameObjectsStorage[lastUsedGameObject.CoordinatorNo] = new GameObjects()
            {
                CoordinateNo = lastUsedGameObject.CoordinatorNo,
                GameObject = lastUsedGameObject
            };
        }

        public void CalculateGameCondition()
        {
            int currentPosition = lastUsedGameObject.CoordinatorNo;

            int topUpperHorizontal = CalculateHorizontalUpper(currentPosition);
            int topUpperVertical = CalculateVerticalUpper(currentPosition);

            var result = CalculateHorizontalSegmentsWin(currentPosition, topUpperHorizontal);

            if (!result)
                result = CalculateVerticalSegmentsWin(currentPosition, topUpperVertical);

            if (!result)
                result = CalculateDiagonialSegmentsWinCondition(currentPosition, DiagonialSide.RightToLeft);

            if (!result)
                result = CalculateDiagonialSegmentsWinCondition(currentPosition, DiagonialSide.LeftToRight);

            WinAction.Invoke(result);
        }


        private bool CalculateHorizontalSegmentsWin(int position, int topUpperHorizontal)
        {
            int posInArray = topUpperHorizontal;
            int? currentMoveValue = gameObjectsStorage[position]?.GameObject.GameObjectCode;

            List<int> winConditionValues = new List<int>();
            for (int i = 0; i < matrixLinearSize; i++)
            {
                int? value = gameObjectsStorage[posInArray]?.GameObject.GameObjectCode;

                posInArray -= 1;

                if (value != null)
                {
                    //optimize for performance change refactor to use it
                    //is one is different from currents position value can't win
                    if (currentMoveValue != value.Value)
                        return false;

                    winConditionValues.Add(value.Value);
                }

            }

            if (winConditionValues.Count == matrixLinearSize)
                return true;
            else
                return false;
                
        }

        private bool CalculateVerticalSegmentsWin(int position, int toUpperVertical)
        {
            int posInArray = toUpperVertical;
            int? currentMoveValue = gameObjectsStorage[position]?.GameObject.GameObjectCode;

            List<int> winConditionValues = new List<int>();
            for (int i = 0; i < matrixLinearSize; i++)
            {
                int? value = gameObjectsStorage[posInArray]?.GameObject.GameObjectCode;

                posInArray -= matrixLinearSize;

                if (value != null)
                {
                    if (currentMoveValue != value.Value) //is one is different then can't win
                        return false;

                    winConditionValues.Add(value.Value);
                }

            }

            if (winConditionValues.Count == matrixLinearSize)
                return true;
            else
                return false;

        }

      


        private bool CalculateDiagonialSegmentsWinCondition(int position, DiagonialSide diagonialSide)
        {
            //upper right to bottom left 
            //n = matrix size -1
            //for each n check value
            int? currentMoveValue = gameObjectsStorage[position]?.GameObject.GameObjectCode;

            int step = 0;
            int nPosition = 0;

            switch (diagonialSide)
            {
                case DiagonialSide.RightToLeft:
                    step = matrixLinearSize - 1;
                    nPosition = matrixLinearSize - 1;
                    break;
                case DiagonialSide.LeftToRight:
                    step = matrixLinearSize + 1;
                    //nPosition is always 0 from Left To Right
                    break;
            }



            List<int> winConditionValues = new List<int>();
            for (int i = 0; i < matrixLinearSize; i++)
            {
                int? value = gameObjectsStorage[nPosition]?.GameObject.GameObjectCode;

                nPosition += step;

                if (value != null)
                {
                    if (currentMoveValue != value.Value) //is one is different then can't win
                        return false;

                    winConditionValues.Add(value.Value);
                }
            }

            if (winConditionValues.Count == matrixLinearSize)
                return true;
            else
                return false;

        }

        //need refactor to find upper from matrix size
        private int CalculateHorizontalUpper(int position)
        {
            /*
             * what if 
               0 1 2
               3 4 5
               6 7 8

               will be

                0 3 6
                1 4 7
                2 5 8

                then verticals will be horizontals tops (an idea..)

            */
            int topH = position;

            int squareMatrixValue = (int)Math.Pow(matrixLinearSize, 2);


            for (int i = 0; i < matrixLinearSize; i++)
            {
                if (position < squareMatrixValue)
                {
                    topH = squareMatrixValue - 1;
                }
                else
                    break;

                squareMatrixValue -= matrixLinearSize;
            }

            Console.WriteLine($"TopH : {topH}");
            return topH;
        }

        /// <summary>
        /// Calculates the vertical upper.
        /// </summary>
        /// <returns>The vertical upper.</returns>
        /// <param name="position">Position.</param>
        private int CalculateVerticalUpper(int position)
        {
            int topV = position;

            //find value of exponent (real square size) not linear 
            int squareMatrixValue = (int)Math.Pow(matrixLinearSize, 2);

            for (int i = 0; i < squareMatrixValue; i++)
            {
                //check if next value will pass the boundaries of squareSize
                if ((topV + matrixLinearSize) < squareMatrixValue)
                    topV += matrixLinearSize; //if is in boundaries
                else
                    break; // else you find the topV;
            }

            Console.WriteLine($"TopV: {topV}");

            return topV;
        }

    }


}
