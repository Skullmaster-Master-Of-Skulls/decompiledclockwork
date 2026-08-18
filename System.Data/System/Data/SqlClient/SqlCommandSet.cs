using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020002C9 RID: 713
	internal sealed class SqlCommandSet
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x00296C78 File Offset: 0x00296078
		internal SqlCommandSet()
		{
			this._batchCommand = new SqlCommand();
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x00296CB8 File Offset: 0x002960B8
		private SqlCommand BatchCommand
		{
			get
			{
				SqlCommand batchCommand = this._batchCommand;
				if (batchCommand == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return batchCommand;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x00296CD8 File Offset: 0x002960D8
		internal int CommandCount
		{
			get
			{
				return this.CommandList.Count;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x00296CF8 File Offset: 0x002960F8
		private List<SqlCommandSet.LocalCommand> CommandList
		{
			get
			{
				List<SqlCommandSet.LocalCommand> commandList = this._commandList;
				if (commandList == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return commandList;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (set) Token: 0x06002468 RID: 9320 RVA: 0x00296D18 File Offset: 0x00296118
		internal int CommandTimeout
		{
			set
			{
				this.BatchCommand.CommandTimeout = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x00296D38 File Offset: 0x00296138
		// (set) Token: 0x0600246A RID: 9322 RVA: 0x00296D58 File Offset: 0x00296158
		internal SqlConnection Connection
		{
			get
			{
				return this.BatchCommand.Connection;
			}
			set
			{
				this.BatchCommand.Connection = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x00296D78 File Offset: 0x00296178
		internal SqlTransaction Transaction
		{
			set
			{
				this.BatchCommand.Transaction = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x00296D98 File Offset: 0x00296198
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00296DB8 File Offset: 0x002961B8
		internal void Append(SqlCommand command)
		{
			ADP.CheckArgumentNull(command, "command");
			Bid.Trace("<sc.SqlCommandSet.Append|API> %d#, command=%d, parameterCount=%d\n", this.ObjectID, command.ObjectID, command.Parameters.Count);
			string commandText = command.CommandText;
			if (ADP.IsEmpty(commandText))
			{
				throw ADP.CommandTextRequired("Append");
			}
			CommandType commandType = command.CommandType;
			CommandType commandType2 = commandType;
			if (commandType2 == CommandType.Text || commandType2 == CommandType.StoredProcedure)
			{
				SqlParameterCollection sqlParameterCollection = null;
				SqlParameterCollection parameters = command.Parameters;
				if (0 < parameters.Count)
				{
					sqlParameterCollection = new SqlParameterCollection();
					for (int i = 0; i < parameters.Count; i++)
					{
						SqlParameter sqlParameter = new SqlParameter();
						parameters[i].CopyTo(sqlParameter);
						sqlParameterCollection.Add(sqlParameter);
						if (!SqlCommandSet.SqlIdentifierParser.IsMatch(sqlParameter.ParameterName))
						{
							throw ADP.BadParameterName(sqlParameter.ParameterName);
						}
					}
					foreach (object obj in sqlParameterCollection)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj;
						object value = sqlParameter2.Value;
						byte[] array = value as byte[];
						if (array != null)
						{
							int offset = sqlParameter2.Offset;
							int size = sqlParameter2.Size;
							int num = array.Length - offset;
							if (size != 0 && size < num)
							{
								num = size;
							}
							byte[] array2 = new byte[Math.Max(num, 0)];
							Buffer.BlockCopy(array, offset, array2, 0, array2.Length);
							sqlParameter2.Offset = 0;
							sqlParameter2.Value = array2;
						}
						else
						{
							char[] array3 = value as char[];
							if (array3 != null)
							{
								int offset2 = sqlParameter2.Offset;
								int size2 = sqlParameter2.Size;
								int num2 = array3.Length - offset2;
								if (size2 != 0 && size2 < num2)
								{
									num2 = size2;
								}
								char[] array4 = new char[Math.Max(num2, 0)];
								Buffer.BlockCopy(array3, offset2, array4, 0, array4.Length * 2);
								sqlParameter2.Offset = 0;
								sqlParameter2.Value = array4;
							}
							else
							{
								ICloneable cloneable = value as ICloneable;
								if (cloneable != null)
								{
									sqlParameter2.Value = cloneable.Clone();
								}
							}
						}
					}
				}
				int returnParameterIndex = -1;
				if (sqlParameterCollection != null)
				{
					for (int j = 0; j < sqlParameterCollection.Count; j++)
					{
						if (ParameterDirection.ReturnValue == sqlParameterCollection[j].Direction)
						{
							returnParameterIndex = j;
							break;
						}
					}
				}
				SqlCommandSet.LocalCommand item = new SqlCommandSet.LocalCommand(commandText, sqlParameterCollection, returnParameterIndex, command.CommandType);
				this.CommandList.Add(item);
				return;
			}
			if (commandType2 == CommandType.TableDirect)
			{
				throw SQL.NotSupportedCommandType(commandType);
			}
			throw ADP.InvalidCommandType(commandType);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00297048 File Offset: 0x00296448
		internal static void BuildStoredProcedureName(StringBuilder builder, string part)
		{
			if (part != null && 0 < part.Length)
			{
				if ('[' == part[0])
				{
					int num = 0;
					foreach (char c in part)
					{
						if (']' == c)
						{
							num++;
						}
					}
					if (1 == num % 2)
					{
						builder.Append(part);
						return;
					}
				}
				builder.Append("[");
				builder.Append(part.Replace("]", "]]"));
				builder.Append("]");
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x002970D8 File Offset: 0x002964D8
		internal void Clear()
		{
			Bid.Trace("<sc.SqlCommandSet.Clear|API> %d#", this.ObjectID);
			DbCommand batchCommand = this.BatchCommand;
			if (batchCommand != null)
			{
				batchCommand.Parameters.Clear();
				batchCommand.CommandText = null;
			}
			List<SqlCommandSet.LocalCommand> commandList = this._commandList;
			if (commandList != null)
			{
				commandList.Clear();
			}
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00297128 File Offset: 0x00296528
		internal void Dispose()
		{
			Bid.Trace("<sc.SqlCommandSet.Dispose|API> %d#", this.ObjectID);
			SqlCommand batchCommand = this._batchCommand;
			this._commandList = null;
			this._batchCommand = null;
			if (batchCommand != null)
			{
				batchCommand.Dispose();
			}
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x00297168 File Offset: 0x00296568
		internal int ExecuteNonQuery()
		{
			SqlConnection.ExecutePermission.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlCommandSet.ExecuteNonQuery|API> %d#", this.ObjectID);
			int result;
			try
			{
				if (this.Connection.IsContextConnection)
				{
					throw SQL.BatchedUpdatesNotAvailableOnContextConnection();
				}
				this.ValidateCommandBehavior("ExecuteNonQuery", CommandBehavior.Default);
				this.BatchCommand.BatchRPCMode = true;
				this.BatchCommand.ClearBatchCommand();
				this.BatchCommand.Parameters.Clear();
				for (int i = 0; i < this._commandList.Count; i++)
				{
					SqlCommandSet.LocalCommand localCommand = this._commandList[i];
					this.BatchCommand.AddBatchCommand(localCommand.CommandText, localCommand.Parameters, localCommand.CmdType);
				}
				result = this.BatchCommand.ExecuteBatchRPCCommand();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00297258 File Offset: 0x00296658
		internal SqlParameter GetParameter(int commandIndex, int parameterIndex)
		{
			return this.CommandList[commandIndex].Parameters[parameterIndex];
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x00297288 File Offset: 0x00296688
		internal bool GetBatchedAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			error = this.BatchCommand.GetErrors(commandIdentifier);
			int? recordsAffected2 = this.BatchCommand.GetRecordsAffected(commandIdentifier);
			recordsAffected = recordsAffected2.GetValueOrDefault();
			return recordsAffected2 != null;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x002972C8 File Offset: 0x002966C8
		internal int GetParameterCount(int commandIndex)
		{
			return this.CommandList[commandIndex].Parameters.Count;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x002972F8 File Offset: 0x002966F8
		private void ValidateCommandBehavior(string method, CommandBehavior behavior)
		{
			if ((behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection)) != CommandBehavior.Default)
			{
				ADP.ValidateCommandBehavior(behavior);
				throw ADP.NotSupportedCommandBehavior(behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection), method);
			}
		}

		// Token: 0x0400174E RID: 5966
		private const string SqlIdentifierPattern = "^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}＿_@#\\$]*$";

		// Token: 0x0400174F RID: 5967
		private static readonly Regex SqlIdentifierParser = new Regex("^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}＿_@#\\$]*$", RegexOptions.ExplicitCapture | RegexOptions.Singleline);

		// Token: 0x04001750 RID: 5968
		private List<SqlCommandSet.LocalCommand> _commandList = new List<SqlCommandSet.LocalCommand>();

		// Token: 0x04001751 RID: 5969
		private SqlCommand _batchCommand;

		// Token: 0x04001752 RID: 5970
		private static int _objectTypeCount;

		// Token: 0x04001753 RID: 5971
		internal readonly int _objectID = Interlocked.Increment(ref SqlCommandSet._objectTypeCount);

		// Token: 0x020002CA RID: 714
		private sealed class LocalCommand
		{
			// Token: 0x06002477 RID: 9335 RVA: 0x00297348 File Offset: 0x00296748
			internal LocalCommand(string commandText, SqlParameterCollection parameters, int returnParameterIndex, CommandType cmdType)
			{
				this.CommandText = commandText;
				this.Parameters = parameters;
				this.ReturnParameterIndex = returnParameterIndex;
				this.CmdType = cmdType;
			}

			// Token: 0x04001754 RID: 5972
			internal readonly string CommandText;

			// Token: 0x04001755 RID: 5973
			internal readonly SqlParameterCollection Parameters;

			// Token: 0x04001756 RID: 5974
			internal readonly int ReturnParameterIndex;

			// Token: 0x04001757 RID: 5975
			internal readonly CommandType CmdType;
		}
	}
}
