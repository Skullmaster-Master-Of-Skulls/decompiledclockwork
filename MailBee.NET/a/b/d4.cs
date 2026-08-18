using System;
using System.Collections;
using System.Text;

namespace a.b
{
	// Token: 0x02000306 RID: 774
	internal class d4
	{
		// Token: 0x06001B2F RID: 6959 RVA: 0x00076B94 File Offset: 0x00075B94
		public static void a(byte[] A_0, byte A_1)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				A_0[i] = A_1;
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00076BB4 File Offset: 0x00075BB4
		public static void a(char[] A_0, char A_1)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				A_0[i] = A_1;
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00076BD4 File Offset: 0x00075BD4
		public static void a<a>(a[] A_0, a A_1)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				A_0[i] = A_1;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x00076BF8 File Offset: 0x00075BF8
		public static void a(byte[] A_0, int A_1, int A_2, byte A_3)
		{
			d4.a(A_0.Length, A_1, A_2);
			for (int i = A_1; i < A_2; i++)
			{
				A_0[i] = A_3;
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00076C20 File Offset: 0x00075C20
		private static void a(int A_0, int A_1, int A_2)
		{
			if (A_1 > A_2)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"fromIndex(",
					A_1,
					") > toIndex(",
					A_2,
					")"
				}));
			}
			if (A_1 < 0)
			{
				throw new IndexOutOfRangeException("fromIndex(" + A_1 + ")");
			}
			if (A_2 > A_0)
			{
				throw new IndexOutOfRangeException("toIndex(" + A_2 + ")");
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00076CAC File Offset: 0x00075CAC
		public static ArrayList a(Array A_0)
		{
			if (A_0.Length <= 0)
			{
				return new ArrayList();
			}
			ArrayList arrayList = new ArrayList(A_0.Length);
			for (int i = 0; i < A_0.Length; i++)
			{
				arrayList.Add(A_0.GetValue(i));
			}
			return arrayList;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00076CF4 File Offset: 0x00075CF4
		public static void a(int[] A_0, byte A_1)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				A_0[i] = (int)A_1;
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00076D14 File Offset: 0x00075D14
		public static bool a(object A_0, object A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				return false;
			}
			Array array = A_0 as Array;
			Array array2 = A_1 as Array;
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!array.GetValue(i).Equals(array2.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00076D70 File Offset: 0x00075D70
		public static bool a(object[] A_0, object[] A_1)
		{
			if (A_0 == A_1)
			{
				return true;
			}
			if (A_0 == null || A_1 == null)
			{
				return false;
			}
			int num = A_0.Length;
			if (A_1.Length != num)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				object obj = A_0[i];
				object obj2 = A_1[i];
				if (!((obj == null) ? (obj2 == null) : obj.Equals(obj2)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00076DC0 File Offset: 0x00075DC0
		public static void a(object[] A_0, int A_1, int A_2, int A_3)
		{
			if (A_3 <= 0)
			{
				return;
			}
			if (A_1 == A_2)
			{
				return;
			}
			if (A_1 < 0 || A_1 >= A_0.Length)
			{
				throw new ArgumentException("The moveFrom must be a valid array index");
			}
			if (A_2 < 0 || A_2 >= A_0.Length)
			{
				throw new ArgumentException("The moveTo must be a valid array index");
			}
			if (A_1 + A_3 > A_0.Length)
			{
				throw new ArgumentException("Asked to move more entries than the array has");
			}
			if (A_2 + A_3 > A_0.Length)
			{
				throw new ArgumentException("Asked to move to a position that doesn't have enough space");
			}
			object[] array = new object[A_3];
			Array.Copy(A_0, A_1, array, 0, A_3);
			object[] array2;
			int destinationIndex;
			if (A_1 > A_2)
			{
				array2 = new object[A_1 - A_2];
				Array.Copy(A_0, A_2, array2, 0, array2.Length);
				destinationIndex = A_2 + A_3;
			}
			else
			{
				array2 = new object[A_2 - A_1];
				Array.Copy(A_0, A_1 + A_3, array2, 0, array2.Length);
				destinationIndex = A_1;
			}
			Array.Copy(array, 0, A_0, A_2, array.Length);
			Array.Copy(array2, 0, A_0, destinationIndex, array2.Length);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00076E88 File Offset: 0x00075E88
		public static byte[] a(byte[] A_0, int A_1)
		{
			byte[] array = new byte[A_1];
			Array.Copy(A_0, 0, array, 0, Math.Min(A_0.Length, A_1));
			return array;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00076EB0 File Offset: 0x00075EB0
		internal static int[] a(int[] A_0, int A_1, int A_2)
		{
			int num = A_2 - A_1;
			if (num < 0)
			{
				throw new ArgumentException(A_1 + " > " + A_2);
			}
			int[] array = new int[num];
			Array.Copy(A_0, A_1, array, 0, Math.Min(A_0.Length - A_1, num));
			return array;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00076F00 File Offset: 0x00075F00
		internal static byte[] a(byte[] A_0, int A_1, int A_2)
		{
			int num = A_2 - A_1;
			if (num < 0)
			{
				throw new ArgumentException(A_1 + " > " + A_2);
			}
			byte[] array = new byte[num];
			Array.Copy(A_0, A_1, array, 0, Math.Min(A_0.Length - A_1, num));
			return array;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00076F50 File Offset: 0x00075F50
		public static string a(object[] A_0)
		{
			if (A_0 == null)
			{
				return "null";
			}
			int num = A_0.Length - 1;
			if (num == -1)
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			int num2 = 0;
			for (;;)
			{
				stringBuilder.Append(A_0[num2].ToString());
				if (num2 == num)
				{
					break;
				}
				stringBuilder.Append(", ");
				num2++;
			}
			return stringBuilder.Append(']').ToString();
		}
	}
}
