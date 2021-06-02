using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Infrastructure.Services.BulkUpload
{

    public class RowValidationEntity
    {
        private int? _rowNumber;
        private List<string> _validationMsgs;
        public RowValidationEntity()
        {
            _rowNumber = null;
            _validationMsgs = new List<string>();

        }

        public RowValidationEntity(int RowNumber, List<string> Msg)
        {
            _rowNumber = RowNumber;
            _validationMsgs = Msg;

        }

        public int? RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; }
        }

        public List<string> ValidationMsgs
        {
            get { return _validationMsgs; }
            set { _validationMsgs = value; }
        }
    }
}
