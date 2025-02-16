﻿using Swifter.Readers;
using Swifter.Writers;
using System.Data;

namespace Swifter.RW
{
    internal sealed class DataRowInterface<T> : IValueInterface<T> where T : DataRow
    {
        public T ReadValue(IValueReader valueReader)
        {
            if (valueReader is IValueReader<T> tReader)
            {
                return tReader.ReadValue();
            }

            var dataWriter = new DataRowRW<T>();

            valueReader.ReadObject(dataWriter);

            return dataWriter.Content;
        }

        public void WriteValue(IValueWriter valueWriter, T value)
        {
            if (value == null)
            {
                valueWriter.DirectWrite(null);

                return;
            }

            if (valueWriter is IValueWriter<T> tWriter)
            {
                tWriter.WriteValue(value);

                return;
            }

            var dataReader = new DataRowRW<T>();

            dataReader.Initialize(value);

            valueWriter.WriteObject(dataReader);
        }
    }
}