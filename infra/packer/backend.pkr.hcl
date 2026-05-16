source "digitalocean" "backend" {
    api_token = var.do_api_token
    image = "debian-13-x64"
    region = "fra1"
    size = "s-1vcpu-512mb-10gb"
    ssh_username = "root"

    snapshot_name = "backend-${formatdate("YYYYMMDDhhmmss", timestamp())}"
}

build {
    sources = ["source.digitalocean.backend"]

    provisioner "ansible" {
        playbook_file = "../ansible/backend.yaml"
    }
}
