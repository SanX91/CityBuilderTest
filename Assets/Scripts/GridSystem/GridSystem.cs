using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilderTest
{
    public class GridSystem : MonoBehaviour, IInitializer
    {
        public int gridSize = 12;
        public Vector2 gridStartPoint;
        public Vector2 gridSpacing;
        Grid[,] grids;
        Vector2[,] gridPositions;

        void CreateGrids()
        {
            grids = new Grid[gridSize, gridSize];
            for (int i=0; i< gridSize; i++)
            {
                for(int j=0; j<gridSize; j++)
                {
                    grids[i,j] = new Grid(new Vector2Int(i, j));
                }
            }
        }

        void PositionGrids()
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

        void OnDrawGizmos()
        {
            PositionGrids();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Vector3 center = new Vector3(gridPositions[i, j].x, 0, gridPositions[i, j].y);
                    Gizmos.DrawSphere(center, 1);
                }
            }
        }

        public bool CanPlaceObject(IPlaceable placeable, Vector2 inputPosition, out Vector2 gridPosition)
        {
            Vector2Int gridId = placeable.StartGridId = GetNearestGridId(inputPosition);
            gridPosition = gridPositions[gridId.x, gridId.y];

            for (int i=0; i<placeable.GridCost().x; i++)
            {
                for (int j = 0; j < placeable.GridCost().y; j++)
                {
                    if(gridId.x+i>=gridSize||gridId.y+j>=gridSize)
                    {
                        return false;
                    }

                    if(grids[gridId.x+i,gridId.y+j].isOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

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

        Vector2Int GetNearestGridId(Vector2 position)
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
