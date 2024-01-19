namespace ESP.Base.Entity
{
	public abstract class BaseEntityWithId
	{
        public int Id { get; set; }
		public DateTime InsertDate { get; set; }
		public int? UpdateUserId { get; set; }
		public DateTime? UpdateDate { get; set; }
		public bool IsActive { get; set; }
	}
}
