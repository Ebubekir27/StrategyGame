using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace StrategyGame
{
    public static class FindPathController  
    {                 
            public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode)
            {
                var toSearch = new List<NodeBase>() { startNode };
                var processed = new List<NodeBase>();
                


                while (toSearch.Any())
                {
                    var current = toSearch[0];
                    foreach (var t in toSearch)
                        if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

                    processed.Add(current);
                    toSearch.Remove(current);                    
                    if (current == targetNode)
                    {
                        var currentPathTile = targetNode;
                        var path = new List<NodeBase>();
                     
                        while (currentPathTile != startNode)
                        {
                            path.Add(currentPathTile);
                            currentPathTile = currentPathTile.Connection;                                                 
                        }                     
                        return path;
                    }

                    foreach (var neighbor in current.Neighbors.Where(t =>( t.CellState==CellStateType.Empty || t.GetUnit==startNode.GetUnit) && !processed.Contains(t)))
                    {
                        var inSearch = toSearch.Contains(neighbor);

                        var costToNeighbor = current.G + current.GetDistance(neighbor);

                        if (!inSearch || costToNeighbor < neighbor.G)
                        {
                            neighbor.SetG(costToNeighbor);
                            neighbor.SetConnection(current);

                            if (!inSearch)
                            {
                                neighbor.SetH(neighbor.GetDistance(targetNode));
                                toSearch.Add(neighbor);                              
                            }
                        }
                    }
                }
                return null;
            }
        }

    }
 