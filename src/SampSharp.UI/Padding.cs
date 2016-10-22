// SampSharp.UI
// Copyright 2016 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using SampSharp.GameMode;

namespace SampSharp.UI
{
    /// <summary>
    ///     Represents padding or margin information associated with a user interface (UI) element.
    /// </summary>
    public struct Padding
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Padding" /> struct using the supplied padding size for all edges.
        /// </summary>
        /// <param name="all">The number of pixels to be used for padding for all edges.</param>
        public Padding(float all) : this(all, all, all, all)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Padding" /> struct using a separate padding size for each edge.
        /// </summary>
        /// <param name="left">The padding size, in pixels, for the left edge.</param>
        /// <param name="top">The padding size, in pixels, for the top edge.</param>
        /// <param name="right">The padding size, in pixels, for the right edge.</param>
        /// <param name="bottom">The padding size, in pixels, for the bottom edge.</param>
        public Padding(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        ///     Gets or sets the padding value for the left edge.
        /// </summary>
        public float Left { get; set; }

        /// <summary>
        ///     Gets or sets the padding value for the right edge.
        /// </summary>
        public float Right { get; set; }

        /// <summary>
        ///     Gets or sets the padding value for the top edge.
        /// </summary>
        public float Top { get; set; }

        /// <summary>
        ///     Gets or sets the padding value for the bottom edge.
        /// </summary>
        public float Bottom { get; set; }

        /// <summary>
        ///     Gets or sets the padding value for all the edges.
        /// </summary>
        public float All
        {
            get { return Left.Equals(Right) && Left.Equals(Top) && Left.Equals(Bottom) ? Left : -1; }
            set
            {
                Left = value;
                Right = value;
                Top = value;
                Bottom = value;
            }
        }

        /// <summary>
        ///     Gets the combined padding for the right and left edges.
        /// </summary>
        public float Horizontal => Left + Right;

        /// <summary>
        ///     Gets the combined padding for the top and bottom edges.
        /// </summary>
        public float Vertical => Top + Bottom;

        /// <summary>
        ///     Gets the padding information in the form of a <see cref="Vector2" />.
        /// </summary>
        public Vector2 Size => new Vector2(Horizontal, Vertical);

        #region Equality members

        public bool Equals(Padding other)
        {
            return Left.Equals(other.Left) && Right.Equals(other.Right) && Top.Equals(other.Top) && Bottom.Equals(other.Bottom);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Padding && Equals((Padding) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode*397) ^ Right.GetHashCode();
                hashCode = (hashCode*397) ^ Top.GetHashCode();
                hashCode = (hashCode*397) ^ Bottom.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Padding left, Padding right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Padding left, Padding right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}