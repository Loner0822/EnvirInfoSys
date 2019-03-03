namespace PublishSys
{
    class AdminUnit
    {
        public string Id { set; get; }
        public string Pid { set; get; }
        public string Level { set; get; }
        public string Name { set; get; }

        public AdminUnit(string id, string pid, string level, string name)
        {
            this.Id = id;
            this.Pid = pid;
            this.Level = level;
            this.Name = name;
        }
    }
}
