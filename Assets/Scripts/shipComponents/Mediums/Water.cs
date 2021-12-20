namespace shipComponents.Mediums {
	public class Water : Coolant {
		public Water() {
			temperature = 30;
			heatCapacity = 60;
			transferRate = 3;
		}

		public override string ToString() {
			return $"{temperature} degrees";
		}
	}
}