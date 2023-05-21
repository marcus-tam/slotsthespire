namespace _Scripts.MapManager
{
    public class Path {
        public Point startPoint;
        public Point endPoint;
    
        public Path(Point start, Point end) {
            startPoint = start;
            endPoint = end;
        }
    
        public Point GetStartPoint() {
            return startPoint;
        }
    
        public Point GetEndPoint() {
            return endPoint;
        }
        
    }

}