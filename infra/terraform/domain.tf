data "digitalocean_domain" "domain" {
    name = var.domain
}

resource "digitalocean_certificate" "certificate" {
    name = "certificate"
    type = "lets_encrypt"
    domains = [
        var.domain
    ]
}