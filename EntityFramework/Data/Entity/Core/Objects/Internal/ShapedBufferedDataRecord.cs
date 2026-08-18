using System;
using System.Collections;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x020000D0 RID: 208
	internal class ShapedBufferedDataRecord : BufferedDataRecord
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x0001EBF4 File Offset: 0x0001CDF4
		protected ShapedBufferedDataRecord()
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		internal static BufferedDataRecord Initialize(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader, Type[] columnTypes, bool[] nullableColumns)
		{
			ShapedBufferedDataRecord shapedBufferedDataRecord = new ShapedBufferedDataRecord();
			shapedBufferedDataRecord.ReadMetadata(providerManifestToken, providerServices, reader);
			DbSpatialDataReader spatialDataReader = null;
			if (columnTypes.Any((Type t) => t == typeof(DbGeography) || t == typeof(DbGeometry)))
			{
				spatialDataReader = providerServices.GetSpatialDataReader(reader, providerManifestToken);
			}
			return shapedBufferedDataRecord.Initialize(reader, spatialDataReader, columnTypes, nullableColumns);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		internal static Task<BufferedDataRecord> InitializeAsync(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader, Type[] columnTypes, bool[] nullableColumns, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ShapedBufferedDataRecord shapedBufferedDataRecord = new ShapedBufferedDataRecord();
			shapedBufferedDataRecord.ReadMetadata(providerManifestToken, providerServices, reader);
			DbSpatialDataReader spatialDataReader = null;
			if (columnTypes.Any((Type t) => t == typeof(DbGeography) || t == typeof(DbGeometry)))
			{
				spatialDataReader = providerServices.GetSpatialDataReader(reader, providerManifestToken);
			}
			return shapedBufferedDataRecord.InitializeAsync(reader, spatialDataReader, columnTypes, nullableColumns, cancellationToken);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private BufferedDataRecord Initialize(DbDataReader reader, DbSpatialDataReader spatialDataReader, Type[] columnTypes, bool[] nullableColumns)
		{
			this.InitializeFields(columnTypes, nullableColumns);
			while (reader.Read())
			{
				this._currentRowNumber++;
				if (this._rowCapacity == this._currentRowNumber)
				{
					this.DoubleBufferCapacity();
				}
				int num = Math.Max(columnTypes.Length, nullableColumns.Length);
				for (int i = 0; i < num; i++)
				{
					if (i < this._columnTypeCases.Length)
					{
						switch (this._columnTypeCases[i])
						{
						case ShapedBufferedDataRecord.TypeCase.Empty:
							if (nullableColumns[i])
							{
								this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Bool:
							if (!nullableColumns[i])
							{
								this.ReadBool(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadBool(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Byte:
							if (!nullableColumns[i])
							{
								this.ReadByte(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadByte(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Char:
							if (!nullableColumns[i])
							{
								this.ReadChar(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadChar(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.DateTime:
							if (!nullableColumns[i])
							{
								this.ReadDateTime(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadDateTime(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Decimal:
							if (!nullableColumns[i])
							{
								this.ReadDecimal(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadDecimal(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Double:
							if (!nullableColumns[i])
							{
								this.ReadDouble(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadDouble(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Float:
							if (!nullableColumns[i])
							{
								this.ReadFloat(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadFloat(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Guid:
							if (!nullableColumns[i])
							{
								this.ReadGuid(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadGuid(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Short:
							if (!nullableColumns[i])
							{
								this.ReadShort(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadShort(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Int:
							if (!nullableColumns[i])
							{
								this.ReadInt(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadInt(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.Long:
							if (!nullableColumns[i])
							{
								this.ReadLong(reader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadLong(reader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.DbGeography:
							if (!nullableColumns[i])
							{
								this.ReadGeography(spatialDataReader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadGeography(spatialDataReader, i);
								goto IL_534;
							}
							goto IL_534;
						case ShapedBufferedDataRecord.TypeCase.DbGeometry:
							if (!nullableColumns[i])
							{
								this.ReadGeometry(spatialDataReader, i);
								goto IL_534;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadGeometry(spatialDataReader, i);
								goto IL_534;
							}
							goto IL_534;
						}
						if (nullableColumns[i])
						{
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadObject(reader, i);
							}
						}
						else
						{
							this.ReadObject(reader, i);
						}
					}
					else if (nullableColumns[i])
					{
						this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i);
					}
					IL_534:;
				}
			}
			this._bools = new BitArray(this._tempBools);
			this._tempBools = null;
			this._nulls = new BitArray(this._tempNulls);
			this._tempNulls = null;
			this._rowCount = this._currentRowNumber + 1;
			this._currentRowNumber = -1;
			return this;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000215C8 File Offset: 0x0001F7C8
		private async Task<BufferedDataRecord> InitializeAsync(DbDataReader reader, DbSpatialDataReader spatialDataReader, Type[] columnTypes, bool[] nullableColumns, CancellationToken cancellationToken)
		{
			this.InitializeFields(columnTypes, nullableColumns);
			for (;;)
			{
				System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>();
				if (!(await cultureAwaiter))
				{
					break;
				}
				cancellationToken.ThrowIfCancellationRequested();
				this._currentRowNumber++;
				if (this._rowCapacity == this._currentRowNumber)
				{
					this.DoubleBufferCapacity();
				}
				int columnCount = (columnTypes.Length > nullableColumns.Length) ? columnTypes.Length : nullableColumns.Length;
				for (int i = 0; i < columnCount; i++)
				{
					if (i < this._columnTypeCases.Length)
					{
						switch (this._columnTypeCases[i])
						{
						case ShapedBufferedDataRecord.TypeCase.Empty:
							if (nullableColumns[i])
							{
								bool[] tempNulls = this._tempNulls;
								int num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
								cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
								tempNulls[num] = await cultureAwaiter;
								goto IL_219A;
							}
							goto IL_219A;
						case ShapedBufferedDataRecord.TypeCase.Bool:
						{
							if (!nullableColumns[i])
							{
								await this.ReadBoolAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							bool[] tempNulls2 = this._tempNulls;
							int num2 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj = await cultureAwaiter;
							tempNulls2[num2] = obj;
							if (obj == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadBoolAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Byte:
						{
							if (!nullableColumns[i])
							{
								await this.ReadByteAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							bool[] tempNulls3 = this._tempNulls;
							int num3 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							object obj2 = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							tempNulls3[num3] = obj2;
							if (obj2 == null)
							{
								await this.ReadByteAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Char:
						{
							if (!nullableColumns[i])
							{
								await this.ReadCharAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							bool[] tempNulls4 = this._tempNulls;
							int num4 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							object obj3 = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							tempNulls4[num4] = obj3;
							if (obj3 == null)
							{
								await this.ReadCharAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.DateTime:
						{
							if (!nullableColumns[i])
							{
								await this.ReadDateTimeAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							bool[] tempNulls5 = this._tempNulls;
							int num5 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							object obj4 = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							tempNulls5[num5] = obj4;
							if (obj4 == null)
							{
								await this.ReadDateTimeAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Decimal:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadDecimalAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls6 = this._tempNulls;
							int num6 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							object obj5 = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							tempNulls6[num6] = obj5;
							if (obj5 == null)
							{
								await this.ReadDecimalAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Double:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadDoubleAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls7 = this._tempNulls;
							int num7 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj6 = await cultureAwaiter;
							tempNulls7[num7] = obj6;
							if (obj6 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadDoubleAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Float:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadFloatAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls8 = this._tempNulls;
							int num8 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj7 = await cultureAwaiter;
							tempNulls8[num8] = obj7;
							if (obj7 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadFloatAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Guid:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadGuidAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls9 = this._tempNulls;
							int num9 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj8 = await cultureAwaiter;
							tempNulls9[num9] = obj8;
							if (obj8 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadGuidAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Short:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadShortAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls10 = this._tempNulls;
							int num10 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj9 = await cultureAwaiter;
							tempNulls10[num10] = obj9;
							if (obj9 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadShortAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Int:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadIntAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls11 = this._tempNulls;
							int num11 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj10 = await cultureAwaiter;
							tempNulls11[num11] = obj10;
							if (obj10 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadIntAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.Long:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadLongAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls12 = this._tempNulls;
							int num12 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj11 = await cultureAwaiter;
							tempNulls12[num12] = obj11;
							if (obj11 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadLongAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.DbGeography:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadGeographyAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls13 = this._tempNulls;
							int num13 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj12 = await cultureAwaiter;
							tempNulls13[num13] = obj12;
							if (obj12 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadGeographyAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						case ShapedBufferedDataRecord.TypeCase.DbGeometry:
						{
							if (!nullableColumns[i])
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadGeometryAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							bool[] tempNulls14 = this._tempNulls;
							int num14 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj13 = await cultureAwaiter;
							tempNulls14[num14] = obj13;
							if (obj13 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadGeometryAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
								goto IL_219A;
							}
							goto IL_219A;
						}
						}
						if (nullableColumns[i])
						{
							bool[] tempNulls15 = this._tempNulls;
							int num15 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							object obj14 = await cultureAwaiter;
							tempNulls15[num15] = obj14;
							if (obj14 == null)
							{
								System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadObjectAsync(reader, i, cancellationToken).WithCurrentCulture();
								await cultureAwaiter2;
							}
						}
						else
						{
							System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter cultureAwaiter2 = this.ReadObjectAsync(reader, i, cancellationToken).WithCurrentCulture();
							await cultureAwaiter2;
						}
					}
					else if (nullableColumns[i])
					{
						bool[] tempNulls16 = this._tempNulls;
						int num16 = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
						cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
						tempNulls16[num16] = await cultureAwaiter;
					}
					IL_219A:;
				}
			}
			this._bools = new BitArray(this._tempBools);
			this._tempBools = null;
			this._nulls = new BitArray(this._tempNulls);
			this._tempNulls = null;
			this._rowCount = this._currentRowNumber + 1;
			this._currentRowNumber = -1;
			return this;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00021638 File Offset: 0x0001F838
		private void InitializeFields(Type[] columnTypes, bool[] nullableColumns)
		{
			this._columnTypeCases = Enumerable.Repeat<ShapedBufferedDataRecord.TypeCase>(ShapedBufferedDataRecord.TypeCase.Empty, columnTypes.Length).ToArray<ShapedBufferedDataRecord.TypeCase>();
			int count = Math.Max(base.FieldCount, Math.Max(columnTypes.Length, nullableColumns.Length));
			this._ordinalToIndexMap = Enumerable.Repeat<int>(-1, count).ToArray<int>();
			for (int i = 0; i < columnTypes.Length; i++)
			{
				Type left = columnTypes[i];
				if (!(left == null))
				{
					if (left == typeof(bool))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Bool;
						this._ordinalToIndexMap[i] = this._boolCount;
						this._boolCount++;
					}
					else if (left == typeof(byte))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Byte;
						this._ordinalToIndexMap[i] = this._byteCount;
						this._byteCount++;
					}
					else if (left == typeof(char))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Char;
						this._ordinalToIndexMap[i] = this._charCount;
						this._charCount++;
					}
					else if (left == typeof(DateTime))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.DateTime;
						this._ordinalToIndexMap[i] = this._dateTimeCount;
						this._dateTimeCount++;
					}
					else if (left == typeof(decimal))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Decimal;
						this._ordinalToIndexMap[i] = this._decimalCount;
						this._decimalCount++;
					}
					else if (left == typeof(double))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Double;
						this._ordinalToIndexMap[i] = this._doubleCount;
						this._doubleCount++;
					}
					else if (left == typeof(float))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Float;
						this._ordinalToIndexMap[i] = this._floatCount;
						this._floatCount++;
					}
					else if (left == typeof(Guid))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Guid;
						this._ordinalToIndexMap[i] = this._guidCount;
						this._guidCount++;
					}
					else if (left == typeof(short))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Short;
						this._ordinalToIndexMap[i] = this._shortCount;
						this._shortCount++;
					}
					else if (left == typeof(int))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Int;
						this._ordinalToIndexMap[i] = this._intCount;
						this._intCount++;
					}
					else if (left == typeof(long))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Long;
						this._ordinalToIndexMap[i] = this._longCount;
						this._longCount++;
					}
					else
					{
						if (left == typeof(DbGeography))
						{
							this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.DbGeography;
						}
						else if (left == typeof(DbGeometry))
						{
							this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.DbGeometry;
						}
						else
						{
							this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Object;
						}
						this._ordinalToIndexMap[i] = this._objectCount;
						this._objectCount++;
					}
				}
			}
			this._tempBools = new bool[this._rowCapacity * this._boolCount];
			this._bytes = new byte[this._rowCapacity * this._byteCount];
			this._chars = new char[this._rowCapacity * this._charCount];
			this._dateTimes = new DateTime[this._rowCapacity * this._dateTimeCount];
			this._decimals = new decimal[this._rowCapacity * this._decimalCount];
			this._doubles = new double[this._rowCapacity * this._doubleCount];
			this._floats = new float[this._rowCapacity * this._floatCount];
			this._guids = new Guid[this._rowCapacity * this._guidCount];
			this._shorts = new short[this._rowCapacity * this._shortCount];
			this._ints = new int[this._rowCapacity * this._intCount];
			this._longs = new long[this._rowCapacity * this._longCount];
			this._objects = new object[this._rowCapacity * this._objectCount];
			this._nullOrdinalToIndexMap = Enumerable.Repeat<int>(-1, count).ToArray<int>();
			for (int j = 0; j < nullableColumns.Length; j++)
			{
				if (nullableColumns[j])
				{
					this._nullOrdinalToIndexMap[j] = this._nullCount;
					this._nullCount++;
				}
			}
			this._tempNulls = new bool[this._rowCapacity * this._nullCount];
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00021B18 File Offset: 0x0001FD18
		private void DoubleBufferCapacity()
		{
			this._rowCapacity <<= 1;
			bool[] array = new bool[this._tempBools.Length << 1];
			Array.Copy(this._tempBools, array, this._tempBools.Length);
			this._tempBools = array;
			byte[] array2 = new byte[this._bytes.Length << 1];
			Array.Copy(this._bytes, array2, this._bytes.Length);
			this._bytes = array2;
			char[] array3 = new char[this._chars.Length << 1];
			Array.Copy(this._chars, array3, this._chars.Length);
			this._chars = array3;
			DateTime[] array4 = new DateTime[this._dateTimes.Length << 1];
			Array.Copy(this._dateTimes, array4, this._dateTimes.Length);
			this._dateTimes = array4;
			decimal[] array5 = new decimal[this._decimals.Length << 1];
			Array.Copy(this._decimals, array5, this._decimals.Length);
			this._decimals = array5;
			double[] array6 = new double[this._doubles.Length << 1];
			Array.Copy(this._doubles, array6, this._doubles.Length);
			this._doubles = array6;
			float[] array7 = new float[this._floats.Length << 1];
			Array.Copy(this._floats, array7, this._floats.Length);
			this._floats = array7;
			Guid[] array8 = new Guid[this._guids.Length << 1];
			Array.Copy(this._guids, array8, this._guids.Length);
			this._guids = array8;
			short[] array9 = new short[this._shorts.Length << 1];
			Array.Copy(this._shorts, array9, this._shorts.Length);
			this._shorts = array9;
			int[] array10 = new int[this._ints.Length << 1];
			Array.Copy(this._ints, array10, this._ints.Length);
			this._ints = array10;
			long[] array11 = new long[this._longs.Length << 1];
			Array.Copy(this._longs, array11, this._longs.Length);
			this._longs = array11;
			object[] array12 = new object[this._objects.Length << 1];
			Array.Copy(this._objects, array12, this._objects.Length);
			this._objects = array12;
			bool[] array13 = new bool[this._tempNulls.Length << 1];
			Array.Copy(this._tempNulls, array13, this._tempNulls.Length);
			this._tempNulls = array13;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00021D7D File Offset: 0x0001FF7D
		private void ReadBool(DbDataReader reader, int ordinal)
		{
			this._tempBools[this._currentRowNumber * this._boolCount + this._ordinalToIndexMap[ordinal]] = reader.GetBoolean(ordinal);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00021EF4 File Offset: 0x000200F4
		private async Task ReadBoolAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._tempBools[this._currentRowNumber * this._boolCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<bool>(ordinal, cancellationToken).WithCurrentCulture<bool>();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00021F52 File Offset: 0x00020152
		private void ReadByte(DbDataReader reader, int ordinal)
		{
			this._bytes[this._currentRowNumber * this._byteCount + this._ordinalToIndexMap[ordinal]] = reader.GetByte(ordinal);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000220C8 File Offset: 0x000202C8
		private async Task ReadByteAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._bytes[this._currentRowNumber * this._byteCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<byte>(ordinal, cancellationToken).WithCurrentCulture<byte>();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00022126 File Offset: 0x00020326
		private void ReadChar(DbDataReader reader, int ordinal)
		{
			this._chars[this._currentRowNumber * this._charCount + this._ordinalToIndexMap[ordinal]] = reader.GetChar(ordinal);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0002229C File Offset: 0x0002049C
		private async Task ReadCharAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._chars[this._currentRowNumber * this._charCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<char>(ordinal, cancellationToken).WithCurrentCulture<char>();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000222FA File Offset: 0x000204FA
		private void ReadDateTime(DbDataReader reader, int ordinal)
		{
			this._dateTimes[this._currentRowNumber * this._dateTimeCount + this._ordinalToIndexMap[ordinal]] = reader.GetDateTime(ordinal);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000224A4 File Offset: 0x000206A4
		private async Task ReadDateTimeAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			DateTime[] dateTimes = this._dateTimes;
			int num = this._currentRowNumber * this._dateTimeCount + this._ordinalToIndexMap[ordinal];
			dateTimes[num] = await reader.GetFieldValueAsync<DateTime>(ordinal, cancellationToken).WithCurrentCulture<DateTime>();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00022502 File Offset: 0x00020702
		private void ReadDecimal(DbDataReader reader, int ordinal)
		{
			this._decimals[this._currentRowNumber * this._decimalCount + this._ordinalToIndexMap[ordinal]] = reader.GetDecimal(ordinal);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000226AC File Offset: 0x000208AC
		private async Task ReadDecimalAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			decimal[] decimals = this._decimals;
			int num = this._currentRowNumber * this._decimalCount + this._ordinalToIndexMap[ordinal];
			decimals[num] = await reader.GetFieldValueAsync<decimal>(ordinal, cancellationToken).WithCurrentCulture<decimal>();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0002270A File Offset: 0x0002090A
		private void ReadDouble(DbDataReader reader, int ordinal)
		{
			this._doubles[this._currentRowNumber * this._doubleCount + this._ordinalToIndexMap[ordinal]] = reader.GetDouble(ordinal);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00022880 File Offset: 0x00020A80
		private async Task ReadDoubleAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._doubles[this._currentRowNumber * this._doubleCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<double>(ordinal, cancellationToken).WithCurrentCulture<double>();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000228DE File Offset: 0x00020ADE
		private void ReadFloat(DbDataReader reader, int ordinal)
		{
			this._floats[this._currentRowNumber * this._floatCount + this._ordinalToIndexMap[ordinal]] = reader.GetFloat(ordinal);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00022A54 File Offset: 0x00020C54
		private async Task ReadFloatAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._floats[this._currentRowNumber * this._floatCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<float>(ordinal, cancellationToken).WithCurrentCulture<float>();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00022AB2 File Offset: 0x00020CB2
		private void ReadGuid(DbDataReader reader, int ordinal)
		{
			this._guids[this._currentRowNumber * this._guidCount + this._ordinalToIndexMap[ordinal]] = reader.GetGuid(ordinal);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00022C5C File Offset: 0x00020E5C
		private async Task ReadGuidAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			Guid[] guids = this._guids;
			int num = this._currentRowNumber * this._guidCount + this._ordinalToIndexMap[ordinal];
			guids[num] = await reader.GetFieldValueAsync<Guid>(ordinal, cancellationToken).WithCurrentCulture<Guid>();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00022CBA File Offset: 0x00020EBA
		private void ReadShort(DbDataReader reader, int ordinal)
		{
			this._shorts[this._currentRowNumber * this._shortCount + this._ordinalToIndexMap[ordinal]] = reader.GetInt16(ordinal);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00022E30 File Offset: 0x00021030
		private async Task ReadShortAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._shorts[this._currentRowNumber * this._shortCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<short>(ordinal, cancellationToken).WithCurrentCulture<short>();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00022E8E File Offset: 0x0002108E
		private void ReadInt(DbDataReader reader, int ordinal)
		{
			this._ints[this._currentRowNumber * this._intCount + this._ordinalToIndexMap[ordinal]] = reader.GetInt32(ordinal);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00023004 File Offset: 0x00021204
		private async Task ReadIntAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._ints[this._currentRowNumber * this._intCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<int>(ordinal, cancellationToken).WithCurrentCulture<int>();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00023062 File Offset: 0x00021262
		private void ReadLong(DbDataReader reader, int ordinal)
		{
			this._longs[this._currentRowNumber * this._longCount + this._ordinalToIndexMap[ordinal]] = reader.GetInt64(ordinal);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000231D8 File Offset: 0x000213D8
		private async Task ReadLongAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._longs[this._currentRowNumber * this._longCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<long>(ordinal, cancellationToken).WithCurrentCulture<long>();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00023236 File Offset: 0x00021436
		private void ReadObject(DbDataReader reader, int ordinal)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = reader.GetValue(ordinal);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000233AC File Offset: 0x000215AC
		private async Task ReadObjectAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = await reader.GetFieldValueAsync<object>(ordinal, cancellationToken).WithCurrentCulture<object>();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0002340A File Offset: 0x0002160A
		private void ReadGeography(DbSpatialDataReader spatialReader, int ordinal)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = spatialReader.GetGeography(ordinal);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00023580 File Offset: 0x00021780
		private async Task ReadGeographyAsync(DbSpatialDataReader spatialReader, int ordinal, CancellationToken cancellationToken)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = await spatialReader.GetGeographyAsync(ordinal, cancellationToken).WithCurrentCulture<DbGeography>();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000235DE File Offset: 0x000217DE
		private void ReadGeometry(DbSpatialDataReader spatialReader, int ordinal)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = spatialReader.GetGeometry(ordinal);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00023754 File Offset: 0x00021954
		private async Task ReadGeometryAsync(DbSpatialDataReader spatialReader, int ordinal, CancellationToken cancellationToken)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = await spatialReader.GetGeometryAsync(ordinal, cancellationToken).WithCurrentCulture<DbGeometry>();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000237B2 File Offset: 0x000219B2
		public override bool GetBoolean(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Bool)
			{
				return this._bools[this._currentRowNumber * this._boolCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<bool>(ordinal);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000237E8 File Offset: 0x000219E8
		public override byte GetByte(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Byte)
			{
				return this._bytes[this._currentRowNumber * this._byteCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<byte>(ordinal);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002381A File Offset: 0x00021A1A
		public override char GetChar(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Char)
			{
				return this._chars[this._currentRowNumber * this._charCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<char>(ordinal);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002384C File Offset: 0x00021A4C
		public override DateTime GetDateTime(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.DateTime)
			{
				return this._dateTimes[this._currentRowNumber * this._dateTimeCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<DateTime>(ordinal);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00023887 File Offset: 0x00021A87
		public override decimal GetDecimal(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Decimal)
			{
				return this._decimals[this._currentRowNumber * this._decimalCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<decimal>(ordinal);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000238C2 File Offset: 0x00021AC2
		public override double GetDouble(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Double)
			{
				return this._doubles[this._currentRowNumber * this._doubleCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<double>(ordinal);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000238F4 File Offset: 0x00021AF4
		public override float GetFloat(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Float)
			{
				return this._floats[this._currentRowNumber * this._floatCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<float>(ordinal);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00023926 File Offset: 0x00021B26
		public override Guid GetGuid(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Guid)
			{
				return this._guids[this._currentRowNumber * this._guidCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<Guid>(ordinal);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00023962 File Offset: 0x00021B62
		public override short GetInt16(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Short)
			{
				return this._shorts[this._currentRowNumber * this._shortCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<short>(ordinal);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00023995 File Offset: 0x00021B95
		public override int GetInt32(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Int)
			{
				return this._ints[this._currentRowNumber * this._intCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<int>(ordinal);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000239C8 File Offset: 0x00021BC8
		public override long GetInt64(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Long)
			{
				return this._longs[this._currentRowNumber * this._longCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<long>(ordinal);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000239FB File Offset: 0x00021BFB
		public override string GetString(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Object)
			{
				return (string)this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<string>(ordinal);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00023A32 File Offset: 0x00021C32
		public override object GetValue(int ordinal)
		{
			return this.GetFieldValue<object>(ordinal);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00023A3B File Offset: 0x00021C3B
		public override int GetValues(object[] values)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00023A44 File Offset: 0x00021C44
		public override T GetFieldValue<T>(int ordinal)
		{
			switch (this._columnTypeCases[ordinal])
			{
			case ShapedBufferedDataRecord.TypeCase.Empty:
				return default(T);
			case ShapedBufferedDataRecord.TypeCase.Bool:
				return (T)((object)this.GetBoolean(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Byte:
				return (T)((object)this.GetByte(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Char:
				return (T)((object)this.GetChar(ordinal));
			case ShapedBufferedDataRecord.TypeCase.DateTime:
				return (T)((object)this.GetDateTime(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Decimal:
				return (T)((object)this.GetDecimal(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Double:
				return (T)((object)this.GetDouble(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Float:
				return (T)((object)this.GetFloat(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Guid:
				return (T)((object)this.GetGuid(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Short:
				return (T)((object)this.GetInt16(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Int:
				return (T)((object)this.GetInt32(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Long:
				return (T)((object)this.GetInt64(ordinal));
			}
			return (T)((object)this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]]);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00023B8B File Offset: 0x00021D8B
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<T>(this.GetFieldValue<T>(ordinal));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00023B99 File Offset: 0x00021D99
		public override bool IsDBNull(int ordinal)
		{
			return this._nulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[ordinal]];
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00023BBC File Offset: 0x00021DBC
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.IsDBNull(ordinal));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00023BCC File Offset: 0x00021DCC
		public override bool Read()
		{
			return base.IsDataReady = (++this._currentRowNumber < this._rowCount);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00023BFB File Offset: 0x00021DFB
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.Read());
		}

		// Token: 0x04000175 RID: 373
		private int _rowCapacity = 1;

		// Token: 0x04000176 RID: 374
		private BitArray _bools;

		// Token: 0x04000177 RID: 375
		private bool[] _tempBools;

		// Token: 0x04000178 RID: 376
		private int _boolCount;

		// Token: 0x04000179 RID: 377
		private byte[] _bytes;

		// Token: 0x0400017A RID: 378
		private int _byteCount;

		// Token: 0x0400017B RID: 379
		private char[] _chars;

		// Token: 0x0400017C RID: 380
		private int _charCount;

		// Token: 0x0400017D RID: 381
		private DateTime[] _dateTimes;

		// Token: 0x0400017E RID: 382
		private int _dateTimeCount;

		// Token: 0x0400017F RID: 383
		private decimal[] _decimals;

		// Token: 0x04000180 RID: 384
		private int _decimalCount;

		// Token: 0x04000181 RID: 385
		private double[] _doubles;

		// Token: 0x04000182 RID: 386
		private int _doubleCount;

		// Token: 0x04000183 RID: 387
		private float[] _floats;

		// Token: 0x04000184 RID: 388
		private int _floatCount;

		// Token: 0x04000185 RID: 389
		private Guid[] _guids;

		// Token: 0x04000186 RID: 390
		private int _guidCount;

		// Token: 0x04000187 RID: 391
		private short[] _shorts;

		// Token: 0x04000188 RID: 392
		private int _shortCount;

		// Token: 0x04000189 RID: 393
		private int[] _ints;

		// Token: 0x0400018A RID: 394
		private int _intCount;

		// Token: 0x0400018B RID: 395
		private long[] _longs;

		// Token: 0x0400018C RID: 396
		private int _longCount;

		// Token: 0x0400018D RID: 397
		private object[] _objects;

		// Token: 0x0400018E RID: 398
		private int _objectCount;

		// Token: 0x0400018F RID: 399
		private int[] _ordinalToIndexMap;

		// Token: 0x04000190 RID: 400
		private BitArray _nulls;

		// Token: 0x04000191 RID: 401
		private bool[] _tempNulls;

		// Token: 0x04000192 RID: 402
		private int _nullCount;

		// Token: 0x04000193 RID: 403
		private int[] _nullOrdinalToIndexMap;

		// Token: 0x04000194 RID: 404
		private ShapedBufferedDataRecord.TypeCase[] _columnTypeCases;

		// Token: 0x020000D1 RID: 209
		private enum TypeCase
		{
			// Token: 0x04000198 RID: 408
			Empty,
			// Token: 0x04000199 RID: 409
			Object,
			// Token: 0x0400019A RID: 410
			Bool,
			// Token: 0x0400019B RID: 411
			Byte,
			// Token: 0x0400019C RID: 412
			Char,
			// Token: 0x0400019D RID: 413
			DateTime,
			// Token: 0x0400019E RID: 414
			Decimal,
			// Token: 0x0400019F RID: 415
			Double,
			// Token: 0x040001A0 RID: 416
			Float,
			// Token: 0x040001A1 RID: 417
			Guid,
			// Token: 0x040001A2 RID: 418
			Short,
			// Token: 0x040001A3 RID: 419
			Int,
			// Token: 0x040001A4 RID: 420
			Long,
			// Token: 0x040001A5 RID: 421
			DbGeography,
			// Token: 0x040001A6 RID: 422
			DbGeometry
		}
	}
}
