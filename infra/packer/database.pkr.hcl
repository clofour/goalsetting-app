source "digitalocean" "database" {
    api_token = var.do_api_token
    image = "debian-13-x64"
    region = "fra1"
    size = "s-1vcpu-512mb-10gb"
    ssh_username = "root"

    snapshot_name = "database-${formatdate('YYYYMMDDhhmmss', timestamp())}"
}

build {
    sources = ["source.digitalocean.database"]

    provisioner "ansible" {
        playbook_file = "../ansible/database.yaml"
    }
}
