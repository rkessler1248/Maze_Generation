using System;
using System.Collections.Generic;
using System.Linq;
using MazeGeneration.Abstractions;
using MazeGeneration.Abstractions.Algorithms.Solvers;

namespace MazeGeneration.Models.Algorithms.Solvers
{
    public class Distances<TCell, TCoordinates> : IDistances<TCell, TCoordinates> where TCell : class, ICell<TCoordinates>
                                                                                  where TCoordinates : ICoordinates
    {
        public Distances( TCell root )
        {
            Root = root;
            Values = new Dictionary<string, int> { [ Root.Id ] = 0 };
            Cells = new Dictionary<string, TCell> { [ Root.Id ] = Root };
        }

        public TCell Root { get; }
        public IDictionary<string, int> Values { get; }
        public IDictionary<string, TCell> Cells { get; }

        public int this[ string cell ]
        {
            get => !string.IsNullOrEmpty( cell ) && Values.ContainsKey( cell ) ? Values[ cell ] : -1;
            set
            {
                if ( string.IsNullOrEmpty( cell ) )
                {
                    throw new ArgumentException( "Cell id cannot be null or empty." );
                }

                Values[ cell ] = value;
            }
        }

        public IList<string> Keys() => Values.Keys.ToList();

        public IDistances<TCell, TCoordinates> PathTo( TCell goal )
        {
            var current = goal;
            var breadcrumbs = new Distances<TCell, TCoordinates>( Root )
            {
                [ current.Id ] = Values[ current.Id ], 
                Cells = { [ current.Id ] = Cells[ current.Id ] }
            };

            while ( !current.Equals( Root ) )
            {
                foreach ( var link in current.Links )
                {
                    if ( Values[ link.Key ] < Values[ current.Id ] )
                    {
                        breadcrumbs[ link.Key ] = Values[ link.Key ];
                        breadcrumbs.Cells[ link.Key ] = Cells[ link.Key ];
                    }

                    current = ( TCell ) link.Value;
                }
            }

            return breadcrumbs;
        }

        public (TCell MaxCell, int MaxDistance) Max()
        {
            var maxDistance = 0;
            var maxCell = Root;
            foreach ( var value in Values )
            {
                if ( value.Value <= maxDistance )
                {
                    continue;
                }

                maxDistance = value.Value;
                maxCell = Cells[ value.Key ];
            }

            return ( maxCell, maxDistance );
        }
    }
}