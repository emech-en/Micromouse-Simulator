using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroMouseSimul.MicroMouse
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Robot
    {
        #region Private Fields
        /// <summary>
        /// Direction of the Robot at a time.
        /// </summary>
        private enumDirection _direction;
        /// <summary>
        /// Column Number of the robot cell location.
        /// </summary>                
        private int _iXLocation;
        /// <summary>
        /// Row Number of the Robot cell location.
        /// </summary>          
        private int _iYLocation;
        #endregion

        #region Public Properties
        /// <summary>
        /// Column Number of the Robot cell location.
        /// </summary>        
        public int XLocation
        {
            get { return _iXLocation; }
        }
        /// <summary>
        /// Direction of the Robot at a time.
        /// </summary>        
        public enumDirection Direction
        {
            get { return _direction; }
        }
        /// <summary>
        /// Row Number of the Robot cell location.
        /// </summary>        
        public int YLocation
        {
            get { return _iYLocation; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// create new instance of Robot with location 0,0 and direction North.
        /// </summary>
        public Robot()
        {
            _iXLocation = 0;
            _iYLocation = 0;
            _direction = enumDirection.North;
        }

        /// <summary>
        /// create a new instance of Robot with the initial values.
        /// </summary>
        /// <param name="pX">cell Column number of the starting location.</param>
        /// <param name="pY">cell Row number of the starting location.</param>
        /// <param name="pDirection">starting Direction of the robot.</param>
        public Robot(int pX, int pY, enumDirection pDirection)
        {
            if (pX > 15 || pX < 0)
            {
                throw new ArgumentOutOfRangeException("pX", "Should be in [0,15]");
            }
            if (pY > 15 || pY < 0)
            {
                throw new ArgumentOutOfRangeException("pY", "Should be in [0,15]");
            }

            _iXLocation = pX;
            _iYLocation = pY;
            _direction = pDirection;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Change the direction of the Robot like it has turned right!
        /// </summary>
        public void TrunRight()
        {
            switch (_direction)
            {
                case enumDirection.North:
                    _direction = enumDirection.East;
                    break;
                case enumDirection.South:
                    _direction = enumDirection.West;
                    break;
                case enumDirection.East:
                    _direction = enumDirection.South;
                    break;
                case enumDirection.West:
                    _direction = enumDirection.North;
                    break;
                default:
                    throw new Exception("Robot Direction is not Valid. But How!? Fuck you!");
            }
        }

        /// <summary>
        /// Change the direction of the Robot like it has turned left!
        /// </summary>
        public void TrunLeft()
        {
            switch (_direction)
            {
                case enumDirection.North:
                    _direction = enumDirection.West;
                    break;
                case enumDirection.South:
                    _direction = enumDirection.East;
                    break;
                case enumDirection.East:
                    _direction = enumDirection.North;
                    break;
                case enumDirection.West:
                    _direction = enumDirection.South;
                    break;
                default:
                    throw new Exception("Robot Direction is not Valid. But How!? Fuck you!");
            }
        }
                
        /// <summary>
        /// Change the direction of the Robot like it has a UTurn.
        /// </summary>
        public void TrunBack()
        {
            switch (_direction)
            {
                case enumDirection.North:
                    _direction = enumDirection.South;
                    break;
                case enumDirection.South:
                    _direction = enumDirection.North;
                    break;
                case enumDirection.East:
                    _direction = enumDirection.West;
                    break;
                case enumDirection.West:
                    _direction = enumDirection.East;
                    break;
                default:
                    throw new Exception("Robot Direction is not Valid. But How!? Fuck you!");
            }
        }

        /// <summary>
        /// Robot will move one cell According to the direction. 
        /// </summary>
        public void Go()
        {
            switch (_direction)
            {
                case enumDirection.North:
                    if (_iYLocation == 15)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        _iYLocation++;
                    }
                    break;
                case enumDirection.South:
                    if (_iYLocation == 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        _iYLocation--;
                    }
                    break;
                case enumDirection.East:
                    if (_iXLocation == 15)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        _iXLocation++;
                    }
                    break;
                case enumDirection.West:
                    if (_iXLocation == 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        _iXLocation--;
                    }
                    break;
                default:
                    throw new Exception("Robot Direction is not Valid. But How!? Fuck you!");
            }
        }
        #endregion

        internal T GetNextCell<T>(T[,] cell, enumSide robotSide)
        {
            switch (robotSide)
            {
                case enumSide.Front:
                    switch (_direction)
                    {
                        case enumDirection.North:
                            if (YLocation == 15)
                                throw new Exception();
                            return cell[YLocation + 1, XLocation];
                        case enumDirection.South:
                            if (YLocation == 0)
                                throw new Exception();
                            return cell[YLocation - 1, XLocation];
                        case enumDirection.East:
                            if (XLocation == 15)
                                throw new Exception();
                            return cell[YLocation, XLocation + 1];
                        case enumDirection.West:
                            if (XLocation == 0)
                                throw new Exception();
                            return cell[YLocation, XLocation - 1];
                        default:
                            throw new Exception();
                    }
                case enumSide.Right:
                    switch (_direction)
                    {
                        case enumDirection.North:
                            if (XLocation == 15)
                                throw new Exception();
                            return cell[YLocation, XLocation + 1];
                        case enumDirection.South:
                            if (XLocation == 0)
                                throw new Exception();
                            return cell[YLocation, XLocation - 1];
                        case enumDirection.East:
                            if (YLocation == 0)
                                throw new Exception();
                            return cell[YLocation - 1, XLocation];
                        case enumDirection.West:
                            if (YLocation == 15)
                                throw new Exception();
                            return cell[YLocation + 1, XLocation];
                        default:
                            throw new Exception();
                    }
                case enumSide.Left:
                    switch (_direction)
                    {
                        case enumDirection.North:
                            if (XLocation == 0)
                                throw new Exception();
                            return cell[YLocation, XLocation - 1];
                        case enumDirection.South:
                            if (XLocation == 15)
                                throw new Exception();
                            return cell[YLocation, XLocation + 1];
                        case enumDirection.East:
                            if (YLocation == 15)
                                throw new Exception();
                            return cell[YLocation + 1, XLocation];
                        case enumDirection.West:
                            if (YLocation == 0)
                                throw new Exception();
                            return cell[YLocation - 1, XLocation];
                        default:
                            throw new Exception();
                    }
                default:
                    throw new Exception();
            }
        }
        internal T GetPrevCell<T>(T[,] cell)
        {
            switch (_direction)
            {
                case enumDirection.North:
                    return cell[YLocation - 1, XLocation];
                case enumDirection.South:
                    return cell[YLocation + 1, XLocation];
                case enumDirection.East:
                    return cell[YLocation, XLocation - 1];
                case enumDirection.West:
                    return cell[YLocation, XLocation + 1];
                default:
                    throw new Exception();
            }
        }
        internal int[] GetNextCellLoc(enumSide robotSide)
        {
            switch (robotSide)
            {
                case enumSide.Front:
                    switch (_direction)
                    {
                        case enumDirection.North:
                            if (YLocation == 15)
                                throw new Exception();
                            return new int[] { YLocation + 1, XLocation };
                        case enumDirection.South:
                            if (YLocation == 0)
                                throw new Exception();
                            return new int[] { YLocation - 1, XLocation };
                        case enumDirection.East:
                            if (XLocation == 15)
                                throw new Exception();
                            return new int[] { YLocation, XLocation + 1 };
                        case enumDirection.West:
                            if (XLocation == 0)
                                throw new Exception();
                            return new int[] { YLocation, XLocation - 1 };
                        default:
                            throw new Exception();
                    }
                case enumSide.Right:
                    switch (_direction)
                    {
                        case enumDirection.North:
                            if (XLocation == 15)
                                throw new Exception();
                            return new int[] { YLocation, XLocation + 1 };
                        case enumDirection.South:
                            if (XLocation == 0)
                                throw new Exception();
                            return new int[] { YLocation, XLocation - 1 };
                        case enumDirection.East:
                            if (YLocation == 0)
                                throw new Exception();
                            return new int[] { YLocation - 1, XLocation };
                        case enumDirection.West:
                            if (YLocation == 15)
                                throw new Exception();
                            return new int[] { YLocation + 1, XLocation };
                        default:
                            throw new Exception();
                    }
                case enumSide.Left:
                    switch (_direction)
                    {
                        case enumDirection.North:
                            if (XLocation == 0)
                                throw new Exception();
                            return new int[] { YLocation, XLocation - 1 };
                        case enumDirection.South:
                            if (XLocation == 15)
                                throw new Exception();
                            return new int[] { YLocation, XLocation + 1 };
                        case enumDirection.East:
                            if (YLocation == 15)
                                throw new Exception();
                            return new int[] { YLocation + 1, XLocation };
                        case enumDirection.West:
                            if (YLocation == 0)
                                throw new Exception();
                            return new int[] { YLocation - 1, XLocation };
                        default:
                            throw new Exception();
                    }
                default:
                    throw new Exception();
            }
        }

    }
}
