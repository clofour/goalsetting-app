import { Outlet } from "react-router";
import { AppShell, Burger, Group, NavLink } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { IconLayoutDashboard, IconCalendar, IconSparkles } from '@tabler/icons-react';

export default function App() {
    const navLinks = [
        { href: "/app/dashboard", label: "Home", icon: IconLayoutDashboard },
        { href: "/app/calendar", label: "Calendar", icon: IconCalendar },
        { href: "/app/goals", label: "Goals", icon: IconSparkles }
    ];

    const [opened, { toggle }] = useDisclosure();

    return (
        <AppShell
            header={{ height: 60 }}
            navbar={{ width: 300, breakpoint: 'sm', collapsed: { mobile: !opened } }}
            padding="md"
        >
            <AppShell.Header>
                <Group h="100%" px="md">
                    <Burger opened={opened} onClick={toggle} hiddenFrom="sm" size="sm" />
                    Header has a burger icon below sm breakpoint
                </Group>
            </AppShell.Header>
            <AppShell.Navbar p="md">
                {navLinks.map((link) => (
                    <NavLink
                        href={link.href}
                        label={link.label}
                        leftSection={<link.icon size={18} />}
                    />
                ))}
            </AppShell.Navbar>
            <AppShell.Main>
                <Outlet />
            </AppShell.Main>
        </AppShell>
    )
}