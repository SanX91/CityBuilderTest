using System;
using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// Grid System
    /// Creates the grid and generates their positions.
    /// Has method for checking if an object(IPlaceable type) can be placed on the grid.
    /// Has method for placing an object on the grid.
    /// </summary>
    public class GridSystem : MonoBehaviour, IInitializer, IGridSystem
    {
        public int gridSize = 12;
        public Vector2 gridStartPoint;
        public Vector2 gridSpacing;
        private Grid[,] grids;
        private Vector2[,] gridPositions;

        /// <summary>
        /// Creates the grids data, based on the size specified.
        /// </summary>
        private void CreateGrids()
        {
            grids = new Grid[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grids[i, j] = new Grid(new Vector2Int(i, j));
                }
            }
        }

        /// <summary>
        /// Creates the grid positions based on the grid starting point and the grid spacing.
        /// Separate from the grid data, as there's no need for persistence of positions.
        /// </summary>
        private void PositionGrids()
        {
            gridPositions = new Vector2[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    gridPositions[i, j] = gridStartPoint + new Vector2(gridSpacing.x * i, gridSpacing.y * j);
                }
            }
        }

        /// <summary>
        /// Checks if an object of type IPlaceable can be placed on the grid.
        /// Returns true, if the object is inside the grid and on unoccupied tiles.
        /// </summary>
        /// <param name="placeable"></param>
        /// <param name="inputPosition"></param>
        /// <param name="gridPosition"></param>
        /// <returns></returns>
        public bool CanPlaceObject(IPlaceable placeable, Vector2 inputPosition, out Vector2 gridPosition)
        {
            Vector2Int gridId = placeable.StartGridId = GetNearestGridId(inputPosition);
            gridPosition = gridPositions[gridId.x, gridId.y];

            for (int i = 0; i < placeable.GridCost().x; i++)
            {
                for (int j = 0; j < placeable.GridCost().y; j++)
                {
                    if (gridId.x + i >= gridSize || gridId.y + j >= gridSize)
                    {
                        return false;
                    }

                    if (grids[gridId.x + i, gridId.y + j].isOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Places or removes an object from the grid.
        /// </summary>
        /// <param name="placeable"></param>
        /// <param name="isPlaced"></param>
        public void TogglePlaceObject(IPlaceable placeable, bool isPlaced)
        {
            Vector2Int gridId = placeable.StartGridId;

            for (int i = 0; i < placeable.GridCost().x; i++)
            {
                for (int j = 0; j < placeable.GridCost().y; j++)
                {
                    grids[gridId.x + i, gridId.y + j].isOccupied = isPlaced;
                }
            }
        }

        /// <summary>
        /// Gets the nearest grid id to the input position, from the stored grid positions.
        /// All the positions are in world space.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector2Int GetNearestGridId(Vector2 position)
        {
            float minDistance = Mathf.Infinity;
            Vector2Int gridId = Vector2Int.zero;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    float distance = Vector2.Distance(position, gridPositions[i, j]);
                    if (distance < minDistance)
                    {
                        gridId = new Vector2Int(i, j);
                        minDistance = distance;
                    }
                }
            }

            return gridId;
        }

        public void Initialize()
        {
            CreateGrids();
            PositionGrids();
        }
    }

    /// <summary>
    /// The Grid class.
    /// Stores the grid id.
    /// Also stores whether the grid is occupied or not.
    /// </summary>
    [Serializable]
    public class Grid
    {
        public Vector2Int id;
        public bool isOccupied;

        public Grid(Vector2Int id)
        {
            this.id = id;
        }
    }
}
