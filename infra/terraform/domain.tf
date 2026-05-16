locals {
    records = {
        frontend = {
            type = "CNAME",
            name = var.frontend_subdomain,
            value = digitalocean_cdn.cdn.endpoint
        },
        backend = {
            type = "A",
            name = var.backend_subdomain,
            value = digitalocean_loadbalancer.backend.ip
        }
    }
}

data "digitalocean_domain" "domain" {
    name = var.domain
}

resource "digitalocean_record" "main" {
    for_each = local.records

    type = each.value.type
    domain = var.domain
    name = each.value.name
    value = each.value.value
}

resource "digitalocean_certificate" "certificate" {
    name = "certificate"
    type = "lets_encrypt"
    domains = [
        var.domain
    ]
}