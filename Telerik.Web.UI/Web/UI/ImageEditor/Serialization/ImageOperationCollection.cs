using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E99 RID: 3737
	public class ImageOperationCollection : Collection<IImageOperation>
	{
		// Token: 0x06008E97 RID: 36503 RVA: 0x002025A6 File Offset: 0x002007A6
		public ImageOperationCollection() : this(new ImageOperationSerializer())
		{
		}

		// Token: 0x06008E98 RID: 36504 RVA: 0x002025B3 File Offset: 0x002007B3
		public ImageOperationCollection(ImageOperationSerializer operationSerializer)
		{
			this._serializer = operationSerializer;
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x002025C4 File Offset: 0x002007C4
		public string Serialize()
		{
			List<string> list = new List<string>(base.Count);
			foreach (IImageOperation operation in this)
			{
				list.Add(this._serializer.Serialize(operation));
			}
			return string.Format("[{0}]", string.Join(",", list.ToArray()));
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x00202640 File Offset: 0x00200840
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public void Deserialize(string value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			object obj = javaScriptSerializer.DeserializeObject(value);
			if (obj is IEnumerable<object>)
			{
				this.FromData(obj as IEnumerable<object>);
				return;
			}
			this.FromData(new object[]
			{
				obj
			});
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x00202684 File Offset: 0x00200884
		public void FromData(IEnumerable<object> data)
		{
			foreach (object obj in data)
			{
				base.Add(this._serializer.FromData(obj as Dictionary<string, object>));
			}
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x002026DC File Offset: 0x002008DC
		public void Sort()
		{
			IComparer comparer = new ImageOperationCollection.ImageOperationIndexComparer();
			ArrayList.Adapter(base.Items as IList).Sort(comparer);
		}

		// Token: 0x040027A8 RID: 10152
		private readonly ImageOperationSerializer _serializer;

		// Token: 0x02000E9A RID: 3738
		private class ImageOperationIndexComparer : IComparer
		{
			// Token: 0x06008E9D RID: 36509 RVA: 0x00202708 File Offset: 0x00200908
			public int Compare(object x, object y)
			{
				IImageOperation imageOperation = (IImageOperation)x;
				IImageOperation imageOperation2 = (IImageOperation)y;
				if (imageOperation.Index == imageOperation2.Index)
				{
					return 0;
				}
				if (imageOperation.Index > imageOperation2.Index)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
