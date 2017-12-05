using libras_connect_domain.Enums;
using System;
using System.Collections.Generic;

namespace libras_connect_domain.Models
{
    public class DataSocket
    {
        public DateTime DateTime { get; set; }
        public ICollection<HandData> HandData { get; set; }
        public ICollection<PXCMHandData.AlertType> Alert { get; set; }
        public IList<float> CntkInput { get; set; }

        public virtual RealsenseTypeEnum RealsenseType
        {
            get
            {
                if (this.Image != null && this.Image.Length > 0)
                {
                    return RealsenseTypeEnum.IMAGE_DATA;
                }
                else if (this.HandData != null && this.HandData.Count > 0)
                {
                    return RealsenseTypeEnum.HAND_DATA;
                }
                else
                {
                    return RealsenseTypeEnum.DEFAULT;
                }
            }
        }

        public byte[] Image { get; set; }

        public override string ToString()
        {
            return string.Format("HandData=[{0}]; Alert=[{1}]; Image=[{2}]; RealsenseType:[{3}];", HandData, Alert, Image, RealsenseType);
        }
    }
}
