// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System;

namespace Net.Htmlbird.Framework.Web.Map
{
	/// <summary>
	/// 表示地图上的矩形边界。
	/// </summary>
	[Serializable]
	public struct MapBounds
	{
		#region 公有字段

		/// <summary>
		/// 表示其属性未被初始化的 <see cref="MapBounds"/> 结构。
		/// </summary>
		public static readonly MapBounds Empty = new MapBounds(0.0, 0.0, 0.0, 0.0);

		#endregion

		#region 构造函数

		/// <summary>
		/// 使用指定的表示东经、西经、南纬、北纬的数字初始化地图上的矩形边界。
		/// </summary>
		/// <param name="east">表示东经的 <see cref="System.Single"/>。</param>
		/// <param name="west">表示西经的 <see cref="System.Single"/>。</param>
		/// <param name="south">表示南纬的 <see cref="System.Single"/>。</param>
		/// <param name="north">表示北纬的 <see cref="System.Single"/>。</param>
		public MapBounds(double east, double west, double south, double north) : this()
		{
			if (east > 180.0 || east < -180.0) throw new ArgumentOutOfRangeException("east", "经度值必须是大于等于 -180.0 且小于等于 180.0 的数字。");
			if (west > 180.0 || west < -180.0) throw new ArgumentOutOfRangeException("west", "经度值必须是大于等于 -180.0 且小于等于 180.0 的数字。");
			if (south > 90.0 || south < -90.0) throw new ArgumentOutOfRangeException("south", "纬度值必须是大于等于 -90.0 且小于等于 90.0 的数字。");
			if (north > 90.0 || north < -90.0) throw new ArgumentOutOfRangeException("north", "纬度值必须是大于等于 -90.0 且小于等于 90.0 的数字。");

			this.East = east;
			this.West = west;
			this.South = south;
			this.North = north;
			this.Location = new MapPoint(this.West, this.North);
		}

		#endregion

		#region 公有方法

		/// <summary>
		/// 确定坐标点是否包含着当前对象区域内。
		/// </summary>
		/// <param name="mLngLat">一个 <see cref="MapPoint"/>。</param>
		/// <returns>如果坐标点包含在当前对象区域之内则返回 true，否则返回 false。</returns>
		public bool Contains(MapPoint mLngLat) { return mLngLat.Lng >= this.West && mLngLat.Lng <= this.East && mLngLat.Lat >= this.South && mLngLat.Lat <= this.North; }

		/// <summary>
		/// 比较两个 <see cref="MapBounds"/> 对象。此结果指定两个 <see cref="MapBounds"/> 对象的 <see cref="MapBounds.East"/>、<see cref="MapBounds.West"/>、<see cref="MapBounds.South"/> 和 <see cref="MapBounds.North"/> 属性的值是否不等。
		/// </summary>
		/// <param name="left">要比较的 <see cref="MapPoint"/>。</param>
		/// <param name="right">要比较的 <see cref="MapPoint"/>。</param>
		/// <returns>如果 <paramref name="left"/> 和 <paramref name="right"/> 的 <see cref="MapBounds.East"/>、<see cref="MapBounds.West"/>、<see cref="MapBounds.South"/> 和 <see cref="MapBounds.North"/> 属性的值不相等，则为 true；否则为 false。</returns>
		public static bool operator !=(MapBounds left, MapBounds right) { return !(left == right); }

		/// <summary>
		/// 比较两个 <see cref="MapBounds"/> 对象。此结果指定两个 <see cref="MapBounds"/> 对象的 <see cref="MapBounds.East"/>、<see cref="MapBounds.West"/>、<see cref="MapBounds.South"/> 和 <see cref="MapBounds.North"/> 属性的值是否相等。
		/// </summary>
		/// <param name="left">要比较的 <see cref="MapBounds"/>。</param>
		/// <param name="right">要比较的 <see cref="MapBounds"/>。</param>
		/// <returns>如果 <paramref name="left"/> 和 <paramref name="right"/> 的 <see cref="MapBounds.East"/>、<see cref="MapBounds.West"/>、<see cref="MapBounds.South"/> 和 <see cref="MapBounds.North"/> 值均相等，则为 true；否则为 false。</returns>
		public static bool operator ==(MapBounds left, MapBounds right) { return ((left.East == right.East) && (left.West == right.West) && (left.South == right.South) && (left.North == right.North)); }

		/// <summary>
		/// 指定此 <see cref="MapBounds"/> 是否包含与指定 <see cref="System.Object"/> 有相同的坐标。
		/// </summary>
		/// <param name="obj">要测试的 <see cref="System.Object"/>。</param>
		/// <returns>如果 <paramref name="obj"/> 为 <see cref="MapBounds"/> 并与此 <see cref="MapBounds"/> 的东经、西经、南纬和北纬的值相等，则为 true。</returns>
		public override bool Equals(object obj)
		{
			if ((obj is MapBounds) == false) return false;

			MapBounds m = (MapBounds)obj;

			return ((m.East == this.East) && (m.West == this.West) && (m.South == this.South) && (m.North == this.North));
		}

		/// <summary>
		/// 返回此 <see cref="MapBounds"/> 的哈希代码。
		/// </summary>
		/// <returns>一个整数值，它指定此 <see cref="MapBounds"/> 的哈希值。</returns>
		public override int GetHashCode() { return this.East.GetHashCode() ^ this.West.GetHashCode() ^ this.South.GetHashCode() ^ this.North.GetHashCode(); }

		/// <summary>
		/// 将此 <see cref="MapBounds"/> 转换为可读字符串。
		/// </summary>
		/// <returns>表示此 <see cref="MapBounds"/> 的字符串。</returns>
		public override string ToString() { return this.ToJsonString(); }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置对象区域的南纬临界值。
		/// </summary>
		public double South { get; set; }

		/// <summary>
		/// 获取或设置对象区域的北纬临界值。
		/// </summary>
		public double North { get; set; }

		/// <summary>
		/// 获取或设置对象区域的东经临界值。
		/// </summary>
		public double East { get; set; }

		/// <summary>
		/// 获取或设置对象区域的西经临界值。
		/// </summary>
		public double West { get; set; }

		/// <summary>
		/// 获取或设置此 <see cref="MapBounds"/> 结构左上角的坐标（西北方的坐标）。
		/// </summary>
		public MapPoint Location { get; private set; }

		/// <summary>
		/// 测试此 Rectangle 的所有数值属性是否都具有零值。 
		/// </summary>
		public bool IsEmpty { get { return this.East == 0 && this.West == 0 && this.South == 0 && this.North == 0; } }

		#endregion
	}
}