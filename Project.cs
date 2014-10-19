using System;
 
public class Project : Model
{
	private string name;
	private DateTime? created; //nullable! Datetime is a struct => not nullable

	public string Name
	{
	    get 
	    {
	       return name; 
	    }
	    set 
	    {
	       name = value; 
	    }
	}

	public DateTime? Created
	{
	    get 
	    {
	       return created; 
	    }
	    set 
	    {
	       created = value; 
	    }
	}

	public void setCreatedFromLong (long time)
	{
		DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		this.Created = start.AddMilliseconds(time).ToLocalTime();
	}

	public Project(string name) 
	{
		this.name = name;
	}

  	public override string ToString()
   	{
		return "Project " + this.name + " created " + this.Created;
   	}

	public override string getType() 
	{
		return "project";
	}
}
