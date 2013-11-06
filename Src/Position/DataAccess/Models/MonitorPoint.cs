using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class MonitorPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int MonitorTypeId { get; set; }
        public decimal? AlarmUp { get; set; }
        public decimal? AlarmDown { get; set; }
        public decimal? RangeUp { get; set; }
        public decimal? RangeDown { get; set; }
        public string Remark { get; set; }
        public int? OriginalId { get; set; }

        public MonitorType MonitorType { get; set; }

        public int Left {
            get {
                return this.X - (MonitorType == null ? 0 : MonitorType.OffsetX);
            }
        }

        public int Top {
            get {
                return this.Y - (MonitorType == null ? 0 : MonitorType.OffsetY);
            }
        }

        public string Image {
            get {
                return MonitorType == null ? "" : MonitorType.Image;
            }
        }

        public override string ToString() {
            return string.Format("{0}, Id: {1}, Info: {2}", Name, Id, Information);
        }

        public MonitorPoint() {
            this.Name = string.Empty;
        }

        public MonitorPoint(int id, int left, int top, int typeId, MonitorType monitorType)
            : this() {
            this.Id = id;
            this.MonitorTypeId = typeId;
            this.X = left + (monitorType == null ? 0 : monitorType.OffsetX);
            this.Y = top + (monitorType == null ? 0 : monitorType.OffsetY);
        }
    }
}
