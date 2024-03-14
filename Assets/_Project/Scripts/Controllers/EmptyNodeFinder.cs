using UnityEngine;

namespace StrategyGame
{

    public static class EmptyNodeFinder
    {

        public static Node FindEmpty(Vector2 clickPos)
        {
            var targetNode = GridManager.Instance.GetCellAtPosition(clickPos);
            var unit = targetNode.GetUnit;

            if (unit == null) return targetNode;

            var emptyNode = GetClosesPoint(unit);

            return emptyNode;
        }

        private static Node GetClosesPoint(Unit unit)
        {
            var gridManager = GridManager.Instance;
            var gridWidth = gridManager._scriptableGrid.GetGridWidth;
            var gridHeight = gridManager._scriptableGrid.GetGridHeight;

            var maxOrbitToControl = Mathf.Max(gridWidth, gridHeight);

            for (int i = 1; i < maxOrbitToControl; i++)
            {
                var emptyNode = FindEmptyNodeInLine(unit, 0, i);
                if (emptyNode != null) return emptyNode;

                emptyNode = FindEmptyNodeInLine(unit, 1, i);
                if (emptyNode != null) return emptyNode;

                emptyNode = FindEmptyNodeInLine(unit, 2, i);
                if (emptyNode != null) return emptyNode;

                emptyNode = FindEmptyNodeInLine(unit, 3, i);
                if (emptyNode != null) return emptyNode;
            }

            return null;
        }

        private static Node FindEmptyNodeInLine(Unit unit, int edge, int orbitMultiplier)
        {
            var gridManager = GridManager.Instance;
            var gridWidth = gridManager._scriptableGrid.GetGridWidth;
            var gridHeight = gridManager._scriptableGrid.GetGridHeight;
            var width = unit.GetUnitWidth;
            var height = unit.GetUnitHeight;
            var unitPosition = unit.CurrentCellPos();

            GetControlPositions(edge, orbitMultiplier, width, height, unitPosition, out var controlNodePosition, out var lastControlNodePosition, out var stepAmount);

            if (IsInBorder(gridWidth, gridHeight, controlNodePosition) == false
                && IsInBorder(gridWidth, gridHeight, lastControlNodePosition) == false) return null;

            while (controlNodePosition != lastControlNodePosition)
            {
                if (IsInBorder(gridWidth, gridHeight, controlNodePosition) == false)
                {
                    controlNodePosition += stepAmount;
                    continue;
                }

                var nodeToControl = gridManager.GetCellAtPosition(controlNodePosition);
                if (nodeToControl.CellState == CellStateType.Empty) return nodeToControl;

                controlNodePosition += stepAmount;
            }

            return null;
        }
        private static bool IsInBorder(int width, int height, Vector2 position)
        {
            return 0 <= position.x && position.x < width && 0 <= position.y && position.y < height;
        }
        private static void GetControlPositions(int edge, int orbitMultiplier, int width, int height, Vector2 unitPosition, out Vector2 controlNodePosition, out Vector2 lastControlNodePosition, out Vector2 stepAmount)
        {
            switch (edge)
            {
                case 0:
                    controlNodePosition = new Vector2(unitPosition.x - orbitMultiplier, unitPosition.y - orbitMultiplier);
                    lastControlNodePosition = new Vector2(unitPosition.x + width + (orbitMultiplier - 1), unitPosition.y - orbitMultiplier);
                    stepAmount = new Vector2(1, 0);
                    break;
                case 1:
                    controlNodePosition = new Vector2(unitPosition.x + width + (orbitMultiplier - 1), unitPosition.y - orbitMultiplier);
                    lastControlNodePosition = new Vector2(unitPosition.x + width + (orbitMultiplier - 1), unitPosition.y + height + (orbitMultiplier - 1));
                    stepAmount = new Vector2(0, 1);
                    break;
                case 2:
                    controlNodePosition = new Vector2(unitPosition.x + width + (orbitMultiplier - 1), unitPosition.y + height + (orbitMultiplier - 1));
                    lastControlNodePosition = new Vector2(unitPosition.x - orbitMultiplier, unitPosition.y + height + (orbitMultiplier - 1));
                    stepAmount = new Vector2(-1, 0);
                    break;
                default:
                    controlNodePosition = new Vector2(unitPosition.x - orbitMultiplier, unitPosition.y + height + (orbitMultiplier - 1));
                    lastControlNodePosition = new Vector2(unitPosition.x - orbitMultiplier, unitPosition.y - orbitMultiplier);
                    stepAmount = new Vector2(0, -1);
                    break;
            }
        }
    }

    
}
