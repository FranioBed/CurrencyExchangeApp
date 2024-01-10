const PROXY_CONFIG = [
  {
    context: [
      "/currencyexchangerate",
    ],
    target: "https://localhost:7128",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
