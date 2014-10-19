using System;
using System.Collections.Generic;
 
public class ProjectCollection : Collection
{
	private List<Project> list = new List<Project>();

	public void add(Project project) 
	{
		list.Add(project);
	}

    public Project this[int i]
    {
        get
        {
            return list[i];
        }
        set
        {
            list[i] = value;
        }
    }

    public Project this[string name]
    {
        get
        {
            for(int i=0;i<list.Count;i++) 
			{
				if(list[i].Name==name) return list[i];
			}
            return null;
        }
    }

	public override string toURL() 
	{
		return "/api/project.json";
	}
}
