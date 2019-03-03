namespace PublishSys
{
    class LevelUnit
    {
        public string Pguid { set; get; }
        public string Upguid { set; get; }
        public string Name { set; get; }

        public LevelUnit(string pguid, string upguid, string name)
        {
            Pguid = pguid;
            Upguid = upguid;
            Name = name;
        }
    }
}
