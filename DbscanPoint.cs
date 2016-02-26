using System;

	//Point container for Dbscan clustering
	public class DbscanPoint
	{
		public bool IsVisited;
		public DatasetItem ClusterPoint;
		public int ClusterId;

		public DbscanPoint(DatasetItem x)
		{
			ClusterPoint = x;
			IsVisited = false; 
			ClusterId = (int)ClusterIds.UNCLASSIFIED;
		}
	}
