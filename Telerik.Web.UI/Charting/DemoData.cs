using System;
using System.Collections.Generic;

namespace Telerik.Charting
{
	// Token: 0x020016FA RID: 5882
	public class DemoData
	{
		// Token: 0x170045BA RID: 17850
		// (get) Token: 0x0600E47C RID: 58492 RVA: 0x0032B3CF File Offset: 0x003295CF
		public List<double> DoubleList
		{
			get
			{
				return this._doubleList;
			}
		}

		// Token: 0x0600E47D RID: 58493 RVA: 0x0032B3F8 File Offset: 0x003295F8
		public DemoData()
		{
			object[,] array = new object[4, 2];
			array[0, 0] = 1.0;
			array[0, 1] = "First";
			array[1, 0] = 12.4;
			array[1, 1] = "Second";
			array[2, 0] = 4.8;
			array[2, 1] = "Third";
			array[3, 0] = 10.3;
			array[3, 1] = "Fourth";
			this.ObjectsArray = array;
			object[,] array2 = new object[4, 3];
			array2[0, 0] = 1;
			array2[0, 1] = 11.0;
			array2[0, 2] = "First";
			array2[1, 0] = 1;
			array2[1, 1] = 2.4;
			array2[1, 2] = "First";
			array2[2, 0] = 2;
			array2[2, 1] = 4.8;
			array2[2, 2] = "Second";
			array2[3, 0] = 2;
			array2[3, 1] = 10.3;
			array2[3, 2] = "Second";
			this.ObjectsArrayCat = array2;
			base..ctor();
			this._doubleList = new List<double>();
			this._doubleList.Add(12.0);
			this._doubleList.Add(22.0);
			this._doubleList.Add(32.0);
			this._doubleList.Add(8.0);
		}

		// Token: 0x040041ED RID: 16877
		private List<double> _doubleList;

		// Token: 0x040041EE RID: 16878
		public double[] DoubleArray = new double[]
		{
			5.0,
			15.4,
			1.8,
			10.3
		};

		// Token: 0x040041EF RID: 16879
		public object[,] ObjectsArray;

		// Token: 0x040041F0 RID: 16880
		public object[,] ObjectsArrayCat;
	}
}
