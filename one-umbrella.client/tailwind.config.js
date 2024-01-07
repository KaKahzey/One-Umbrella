/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        "forest": {
          0 : "#f1eeee",
          100: "#F8F7FF",
          200: "#DAD7CD",
          300: "#A3B18A",
          400: "#588157",
          500: "#3A5A40",
          600: "#344E41"
        }
      },
    },
    plugins: [],
  }
}
