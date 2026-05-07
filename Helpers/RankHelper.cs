namespace DojoManager.Helpers {
    public static class RankHelper {
        public static string Format(decimal rank) {
            if (rank == Math.Floor(rank) && rank >= 1)
                return Ordinal((int)rank) + " Dan";

            int kyu = (int)Math.Round(rank * 10);
            return Ordinal(kyu) + " Kyu";
        }

        private static string Ordinal(int n) => n switch {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => $"{n}th"
        };
    }
}
