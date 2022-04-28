using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GridManagement
{
    public class SudokuGrid : MonoBehaviour
    {
        [SerializeField] private int columns = 0;
        [SerializeField] private int rows = 0;
        [SerializeField] private float squareOffset = 0f;
        [SerializeField] private GameObject gridSquarePrefab;
        [SerializeField] private float gridSquareScale = 1.0f;
        [SerializeField] private Vector2 startPosition = new Vector2(0.0f, 0.0f);
        
        public float squareGap = 0.1f;
        public Color lineHighlightColor;
        
        private List<GameObject> _gridSquares = new List<GameObject>();
        private GameModeDifficulty _gameMode = GameModeDifficulty.Unknown;
        
        private int _selectedGridLevelData = -1;
        private static int _currentStage = -1;
        private SaveManager _saveManager;
        
        void Start()
        {
            _saveManager = SaveManager.Instance;
            
            if (gridSquarePrefab.GetComponent<GridSquare>() == null)
                Debug.LogError("No GridSquare script attached!");
            CreateGrid();
            _gameMode = GameSettings.Instance.GetGameMode();

            SetGridNumber(_gameMode);
            
            ResetCounter.Instance.resetCounted = 1;
            _saveManager.Data.TotalResetCount = ResetCounter.Instance.resetCounted;
            _saveManager.Data.GameLevel = _selectedGridLevelData;
            _saveManager.Data.GameModeDifficulty = _gameMode;
            
            _saveManager.Save();

        }

   

        private void CreateGrid()
        {
            SpawnGridSquares();
            SetSquaresPosition();
        }

        private void SpawnGridSquares()
        {
            int squareIndex = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {

                    var spawnedGridSquarePrefab = Instantiate(gridSquarePrefab, this.transform, true);
                    var gridSquare = spawnedGridSquarePrefab.GetComponent<GridSquare>();
                    gridSquare.SetSquareIndex(squareIndex);
                    spawnedGridSquarePrefab.transform.localScale = new Vector3(gridSquareScale, gridSquareScale, gridSquareScale);
                    
                    _gridSquares.Add(spawnedGridSquarePrefab);
                    squareIndex++;
                }
            }
            
        }

        private void SetSquaresPosition()
        {
            var square_rect = _gridSquares[0].GetComponent<RectTransform>();
            Vector2 offset = new Vector2();
            Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
            bool row_moved = false;
            offset.x = square_rect.rect.width * square_rect.transform.localScale.x + squareOffset;
            offset.y = square_rect.rect.height * square_rect.transform.localScale.y + squareOffset;

            int column_number = 0;
            int row_number = 0;

            foreach (GameObject square in _gridSquares)
            {
                if (column_number + 1 > columns)
                {
                    row_number++;
                    column_number = 0;
                    square_gap_number.x = 0f;
                    row_moved = false;
                }

                var pos_x_offset = offset.x * column_number + (square_gap_number.x * squareGap);
                var pos_y_offset = offset.y * row_number + (square_gap_number.y * squareGap);

                if(column_number > 0 && column_number % 3 == 0)
                {
                    square_gap_number.x++;
                    pos_x_offset += squareGap;
                }
                if(row_number > 0 && row_number % 3 == 0 && row_moved == false)
                {
                    row_moved = true;
                    square_gap_number.y++;
                    pos_y_offset += squareGap;
                }

                square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
                column_number++;
            }
        }
        private void SetGridNumber(GameModeDifficulty difficulty)
        {
            
            if (ResetCounter.Instance.resetCounted == 0)
            {

                _selectedGridLevelData = 0;

                if (_saveManager.HasSave())
                {
                    _selectedGridLevelData = _saveManager.Data.GameLevel;
                }
                else
                {
                    _selectedGridLevelData = Random.Range(0, SudokuData.Instance.SudokuGamesDict[difficulty].Count);
                }

                _currentStage = _selectedGridLevelData;
                
            }

            var savedGridData = _saveManager.Data.CurrentGridData;
            var data = SudokuData.Instance.SudokuGamesDict[difficulty][_currentStage];
            
            var unsolvedData = data.UnsolvedData;
            
            if (savedGridData.Count == unsolvedData.Length )
            {
                for (int i = 0; i < savedGridData.Count; i++)
                {
                    unsolvedData[i] = savedGridData[i];
                }

                data.UnsolvedData = unsolvedData;
            }
            

            SetGridSquareData(
                new SudokuBoardData()
            {
                SolvedData = data.SolvedData,
                UnsolvedData =  unsolvedData,
            });
         
        }

        private void SetGridSquareData(SudokuBoardData data)
        {
            for (int index = 0; index < _gridSquares.Count; index++)
            {

                var solvedData = data.SolvedData[index];
                var unsolvedData = data.UnsolvedData[index];
                
                _gridSquares[index].GetComponent<GridSquare>().SetNumber(unsolvedData);
                _gridSquares[index].GetComponent<GridSquare>().SetCorrectNumber(solvedData);
                _gridSquares[index].GetComponent<GridSquare>().SetHasDefaultValue(unsolvedData != 0 && unsolvedData == solvedData);
            }
        }

        private void OnEnable()
        {
            GameEvents.OnSquareSelected += OnSquareSelected;
            GameEvents.OnCorrectNumber += OnSquareCorrect;
        }

        private void OnDisable()
        {
            GameEvents.OnSquareSelected -= OnSquareSelected;
            GameEvents.OnCorrectNumber -= OnSquareCorrect;

        }

        private void SetSquaresColor(int[] data, Color color)
        {
            foreach (var index in data)
            {

                var square = _gridSquares[index];
                var gridSquare = square.GetComponent<GridSquare>();
                if (!gridSquare.IsSelected() && !gridSquare.HasWrongValue())
                {
                    gridSquare.SetSquareColour(color);
                }
            }
        }

        private void OnSquareCorrect(int squareIndex)
        {
            var data = SudokuData.Instance.SudokuGamesDict[_gameMode][_currentStage];
            
            /* Copy the unsolved data list to another
             * which will be used for storing the current
             * game state
             */
            var oldGridData = _saveManager.Data.CurrentGridData;
            
            var gridData = new List<int>();
            
            if (oldGridData.Count == 0)
            {
                gridData.AddRange(data.UnsolvedData);
            }
            else
            {
                gridData.AddRange(oldGridData);
            }

            var solvedData = data.SolvedData;
            var solvedNumber = solvedData[squareIndex];
            
            // Prevent IndexOutOfBounds error
            if (squareIndex >= gridData.Count)
            {
                return;
            }


            gridData[squareIndex] = solvedNumber;
            _saveManager.Data.CurrentGridData = gridData;
            _saveManager.Save(true);
            
            Debug.Log($"Unsolved Number: {data.UnsolvedData[squareIndex]}, SolvedNumber: {solvedNumber}, SquareIndex: {gridData[squareIndex]}");
            
        }

  

        public void OnSquareSelected(int index)
        {
            var horizontalLine = LineIndicator.instance.GetHorizontalLine(index);
            var verticalLine = LineIndicator.instance.GetVerticalLine(index);
            var square = LineIndicator.instance.GetSquare(index);

            SetSquaresColor(LineIndicator.instance.GetAllSquareIndex(), Color.white);
            SetSquaresColor(horizontalLine, lineHighlightColor);
            SetSquaresColor(verticalLine, lineHighlightColor);
            SetSquaresColor(square, lineHighlightColor);
        }
    }
}
